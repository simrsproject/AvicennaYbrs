/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/26/2022 2:05:04 PM
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
	abstract public class esKirimLISCollection : esEntityCollectionWAuditLog
	{
		public esKirimLISCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "KirimLISCollection";
		}

		#region Query Logic
		protected void InitQuery(esKirimLISQuery query)
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
			this.InitQuery(query as esKirimLISQuery);
		}
		#endregion
		
		virtual public KirimLIS DetachEntity(KirimLIS entity)
		{
			return base.DetachEntity(entity) as KirimLIS;
		}
		
		virtual public KirimLIS AttachEntity(KirimLIS entity)
		{
			return base.AttachEntity(entity) as KirimLIS;
		}
		
		virtual public void Combine(KirimLISCollection collection)
		{
			base.Combine(collection);
		}
		
		new public KirimLIS this[int index]
		{
			get
			{
				return base[index] as KirimLIS;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(KirimLIS);
		}
	}



	[Serializable]
	abstract public class esKirimLIS : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esKirimLISQuery GetDynamicQuery()
		{
			return null;
		}

		public esKirimLIS()
		{

		}

		public esKirimLIS(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String kode)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kode);
			else
				return LoadByPrimaryKeyStoredProcedure(kode);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String kode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kode);
			else
				return LoadByPrimaryKeyStoredProcedure(kode);
		}

		private bool LoadByPrimaryKeyDynamic(System.String kode)
		{
			esKirimLISQuery query = this.GetDynamicQuery();
			query.Where(query.Kode == kode);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String kode)
		{
			esParameters parms = new esParameters();
			parms.Add("kode",kode);
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
						case "ModifiedDate": this.str.ModifiedDate = (string)value; break;							
						case "NoPasien": this.str.NoPasien = (string)value; break;							
						case "KodeKunjungan": this.str.KodeKunjungan = (string)value; break;							
						case "Nama": this.str.Nama = (string)value; break;							
						case "Email": this.str.Email = (string)value; break;							
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;							
						case "UmurTahun": this.str.UmurTahun = (string)value; break;							
						case "UmurBulan": this.str.UmurBulan = (string)value; break;							
						case "UmurHari": this.str.UmurHari = (string)value; break;							
						case "Gender": this.str.Gender = (string)value; break;							
						case "Alamat": this.str.Alamat = (string)value; break;							
						case "Diagnosa": this.str.Diagnosa = (string)value; break;							
						case "TglPeriksa": this.str.TglPeriksa = (string)value; break;							
						case "Pengirim": this.str.Pengirim = (string)value; break;							
						case "PengirimName": this.str.PengirimName = (string)value; break;							
						case "Kelas": this.str.Kelas = (string)value; break;							
						case "KelasName": this.str.KelasName = (string)value; break;							
						case "Ruang": this.str.Ruang = (string)value; break;							
						case "RuangName": this.str.RuangName = (string)value; break;							
						case "CaraBayar": this.str.CaraBayar = (string)value; break;							
						case "CaraBayarName": this.str.CaraBayarName = (string)value; break;							
						case "KodeTarif": this.str.KodeTarif = (string)value; break;							
						case "ISInap": this.str.ISInap = (string)value; break;							
						case "Status": this.str.Status = (string)value; break;							
						case "Update": this.str.Update = (string)value; break;							
						case "NoReg": this.str.NoReg = (string)value; break;							
						case "FlagTaken": this.str.FlagTaken = (string)value; break;							
						case "Nik": this.str.Nik = (string)value; break;							
						case "Catatan": this.str.Catatan = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ModifiedDate":
						
							if (value == null || value is System.DateTime)
								this.ModifiedDate = (System.DateTime?)value;
							break;
						
						case "DateOfBirth":
						
							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
							break;
						
						case "UmurTahun":
						
							if (value == null || value is System.Int32)
								this.UmurTahun = (System.Int32?)value;
							break;
						
						case "UmurBulan":
						
							if (value == null || value is System.Int32)
								this.UmurBulan = (System.Int32?)value;
							break;
						
						case "UmurHari":
						
							if (value == null || value is System.Int32)
								this.UmurHari = (System.Int32?)value;
							break;
						
						case "TglPeriksa":
						
							if (value == null || value is System.DateTime)
								this.TglPeriksa = (System.DateTime?)value;
							break;
						
						case "FlagTaken":
						
							if (value == null || value is System.Int32)
								this.FlagTaken = (System.Int32?)value;
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
		/// Maps to KirimLIS.kode
		/// </summary>
		virtual public System.String Kode
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Kode);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Kode, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.modified_date
		/// </summary>
		virtual public System.DateTime? ModifiedDate
		{
			get
			{
				return base.GetSystemDateTime(KirimLISMetadata.ColumnNames.ModifiedDate);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISMetadata.ColumnNames.ModifiedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.No_Pasien
		/// </summary>
		virtual public System.String NoPasien
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.NoPasien);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.NoPasien, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Kode_Kunjungan
		/// </summary>
		virtual public System.String KodeKunjungan
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.KodeKunjungan);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.KodeKunjungan, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Nama
		/// </summary>
		virtual public System.String Nama
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Nama);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Nama, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Date_of_birth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(KirimLISMetadata.ColumnNames.DateOfBirth);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.UmurTahun
		/// </summary>
		virtual public System.Int32? UmurTahun
		{
			get
			{
				return base.GetSystemInt32(KirimLISMetadata.ColumnNames.UmurTahun);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISMetadata.ColumnNames.UmurTahun, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.UmurBulan
		/// </summary>
		virtual public System.Int32? UmurBulan
		{
			get
			{
				return base.GetSystemInt32(KirimLISMetadata.ColumnNames.UmurBulan);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISMetadata.ColumnNames.UmurBulan, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.UmurHari
		/// </summary>
		virtual public System.Int32? UmurHari
		{
			get
			{
				return base.GetSystemInt32(KirimLISMetadata.ColumnNames.UmurHari);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISMetadata.ColumnNames.UmurHari, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Gender, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Alamat
		/// </summary>
		virtual public System.String Alamat
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Alamat);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Alamat, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Diagnosa
		/// </summary>
		virtual public System.String Diagnosa
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Diagnosa);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Diagnosa, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Tgl_Periksa
		/// </summary>
		virtual public System.DateTime? TglPeriksa
		{
			get
			{
				return base.GetSystemDateTime(KirimLISMetadata.ColumnNames.TglPeriksa);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISMetadata.ColumnNames.TglPeriksa, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Pengirim
		/// </summary>
		virtual public System.String Pengirim
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Pengirim);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Pengirim, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.pengirim_name
		/// </summary>
		virtual public System.String PengirimName
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.PengirimName);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.PengirimName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Kelas
		/// </summary>
		virtual public System.String Kelas
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Kelas);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Kelas, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.kelas_name
		/// </summary>
		virtual public System.String KelasName
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.KelasName);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.KelasName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Ruang
		/// </summary>
		virtual public System.String Ruang
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Ruang);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Ruang, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.ruang_name
		/// </summary>
		virtual public System.String RuangName
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.RuangName);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.RuangName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Cara_Bayar
		/// </summary>
		virtual public System.String CaraBayar
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.CaraBayar);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.CaraBayar, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.cara_bayar_name
		/// </summary>
		virtual public System.String CaraBayarName
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.CaraBayarName);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.CaraBayarName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Kode_Tarif
		/// </summary>
		virtual public System.String KodeTarif
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.KodeTarif);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.KodeTarif, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.IS_Inap
		/// </summary>
		virtual public System.String ISInap
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.ISInap);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.ISInap, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.Status
		/// </summary>
		virtual public System.String Status
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.update
		/// </summary>
		virtual public System.String Update
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Update);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Update, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.No_reg
		/// </summary>
		virtual public System.String NoReg
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.NoReg);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.NoReg, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.FLAG_TAKEN
		/// </summary>
		virtual public System.Int32? FlagTaken
		{
			get
			{
				return base.GetSystemInt32(KirimLISMetadata.ColumnNames.FlagTaken);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISMetadata.ColumnNames.FlagTaken, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.NIK
		/// </summary>
		virtual public System.String Nik
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Nik);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Nik, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS.CATATAN
		/// </summary>
		virtual public System.String Catatan
		{
			get
			{
				return base.GetSystemString(KirimLISMetadata.ColumnNames.Catatan);
			}
			
			set
			{
				base.SetSystemString(KirimLISMetadata.ColumnNames.Catatan, value);
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
			public esStrings(esKirimLIS entity)
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
				
			public System.String NoPasien
			{
				get
				{
					System.String data = entity.NoPasien;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoPasien = null;
					else entity.NoPasien = Convert.ToString(value);
				}
			}
				
			public System.String KodeKunjungan
			{
				get
				{
					System.String data = entity.KodeKunjungan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeKunjungan = null;
					else entity.KodeKunjungan = Convert.ToString(value);
				}
			}
				
			public System.String Nama
			{
				get
				{
					System.String data = entity.Nama;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nama = null;
					else entity.Nama = Convert.ToString(value);
				}
			}
				
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
				
			public System.String DateOfBirth
			{
				get
				{
					System.DateTime? data = entity.DateOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfBirth = null;
					else entity.DateOfBirth = Convert.ToDateTime(value);
				}
			}
				
			public System.String UmurTahun
			{
				get
				{
					System.Int32? data = entity.UmurTahun;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UmurTahun = null;
					else entity.UmurTahun = Convert.ToInt32(value);
				}
			}
				
			public System.String UmurBulan
			{
				get
				{
					System.Int32? data = entity.UmurBulan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UmurBulan = null;
					else entity.UmurBulan = Convert.ToInt32(value);
				}
			}
				
			public System.String UmurHari
			{
				get
				{
					System.Int32? data = entity.UmurHari;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UmurHari = null;
					else entity.UmurHari = Convert.ToInt32(value);
				}
			}
				
			public System.String Gender
			{
				get
				{
					System.String data = entity.Gender;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Gender = null;
					else entity.Gender = Convert.ToString(value);
				}
			}
				
			public System.String Alamat
			{
				get
				{
					System.String data = entity.Alamat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Alamat = null;
					else entity.Alamat = Convert.ToString(value);
				}
			}
				
			public System.String Diagnosa
			{
				get
				{
					System.String data = entity.Diagnosa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Diagnosa = null;
					else entity.Diagnosa = Convert.ToString(value);
				}
			}
				
			public System.String TglPeriksa
			{
				get
				{
					System.DateTime? data = entity.TglPeriksa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglPeriksa = null;
					else entity.TglPeriksa = Convert.ToDateTime(value);
				}
			}
				
			public System.String Pengirim
			{
				get
				{
					System.String data = entity.Pengirim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pengirim = null;
					else entity.Pengirim = Convert.ToString(value);
				}
			}
				
			public System.String PengirimName
			{
				get
				{
					System.String data = entity.PengirimName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PengirimName = null;
					else entity.PengirimName = Convert.ToString(value);
				}
			}
				
			public System.String Kelas
			{
				get
				{
					System.String data = entity.Kelas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kelas = null;
					else entity.Kelas = Convert.ToString(value);
				}
			}
				
			public System.String KelasName
			{
				get
				{
					System.String data = entity.KelasName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KelasName = null;
					else entity.KelasName = Convert.ToString(value);
				}
			}
				
			public System.String Ruang
			{
				get
				{
					System.String data = entity.Ruang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ruang = null;
					else entity.Ruang = Convert.ToString(value);
				}
			}
				
			public System.String RuangName
			{
				get
				{
					System.String data = entity.RuangName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RuangName = null;
					else entity.RuangName = Convert.ToString(value);
				}
			}
				
			public System.String CaraBayar
			{
				get
				{
					System.String data = entity.CaraBayar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CaraBayar = null;
					else entity.CaraBayar = Convert.ToString(value);
				}
			}
				
			public System.String CaraBayarName
			{
				get
				{
					System.String data = entity.CaraBayarName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CaraBayarName = null;
					else entity.CaraBayarName = Convert.ToString(value);
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
				
			public System.String ISInap
			{
				get
				{
					System.String data = entity.ISInap;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ISInap = null;
					else entity.ISInap = Convert.ToString(value);
				}
			}
				
			public System.String Status
			{
				get
				{
					System.String data = entity.Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Status = null;
					else entity.Status = Convert.ToString(value);
				}
			}
				
			public System.String Update
			{
				get
				{
					System.String data = entity.Update;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Update = null;
					else entity.Update = Convert.ToString(value);
				}
			}
				
			public System.String NoReg
			{
				get
				{
					System.String data = entity.NoReg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoReg = null;
					else entity.NoReg = Convert.ToString(value);
				}
			}
				
			public System.String FlagTaken
			{
				get
				{
					System.Int32? data = entity.FlagTaken;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlagTaken = null;
					else entity.FlagTaken = Convert.ToInt32(value);
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
			

			private esKirimLIS entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esKirimLISQuery query)
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
				throw new Exception("esKirimLIS can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esKirimLISQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return KirimLISMetadata.Meta();
			}
		}	
		

		public esQueryItem Kode
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Kode, esSystemType.String);
			}
		} 
		
		public esQueryItem ModifiedDate
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.ModifiedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoPasien
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.NoPasien, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeKunjungan
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.KodeKunjungan, esSystemType.String);
			}
		} 
		
		public esQueryItem Nama
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Nama, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem UmurTahun
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.UmurTahun, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UmurBulan
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.UmurBulan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UmurHari
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.UmurHari, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
		
		public esQueryItem Alamat
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Alamat, esSystemType.String);
			}
		} 
		
		public esQueryItem Diagnosa
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Diagnosa, esSystemType.String);
			}
		} 
		
		public esQueryItem TglPeriksa
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.TglPeriksa, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Pengirim
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Pengirim, esSystemType.String);
			}
		} 
		
		public esQueryItem PengirimName
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.PengirimName, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelas
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Kelas, esSystemType.String);
			}
		} 
		
		public esQueryItem KelasName
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.KelasName, esSystemType.String);
			}
		} 
		
		public esQueryItem Ruang
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Ruang, esSystemType.String);
			}
		} 
		
		public esQueryItem RuangName
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.RuangName, esSystemType.String);
			}
		} 
		
		public esQueryItem CaraBayar
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.CaraBayar, esSystemType.String);
			}
		} 
		
		public esQueryItem CaraBayarName
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.CaraBayarName, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTarif
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.KodeTarif, esSystemType.String);
			}
		} 
		
		public esQueryItem ISInap
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.ISInap, esSystemType.String);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Status, esSystemType.String);
			}
		} 
		
		public esQueryItem Update
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Update, esSystemType.String);
			}
		} 
		
		public esQueryItem NoReg
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.NoReg, esSystemType.String);
			}
		} 
		
		public esQueryItem FlagTaken
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.FlagTaken, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Nik
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Nik, esSystemType.String);
			}
		} 
		
		public esQueryItem Catatan
		{
			get
			{
				return new esQueryItem(this, KirimLISMetadata.ColumnNames.Catatan, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("KirimLISCollection")]
	public partial class KirimLISCollection : esKirimLISCollection, IEnumerable<KirimLIS>
	{
		public KirimLISCollection()
		{

		}
		
		public static implicit operator List<KirimLIS>(KirimLISCollection coll)
		{
			List<KirimLIS> list = new List<KirimLIS>();
			
			foreach (KirimLIS emp in coll)
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
				return  KirimLISMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KirimLISQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new KirimLIS(row);
		}

		override protected esEntity CreateEntity()
		{
			return new KirimLIS();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public KirimLISQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KirimLISQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(KirimLISQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public KirimLIS AddNew()
		{
			KirimLIS entity = base.AddNewEntity() as KirimLIS;
			
			return entity;
		}

		public KirimLIS FindByPrimaryKey(System.String kode)
		{
			return base.FindByPrimaryKey(kode) as KirimLIS;
		}


		#region IEnumerable<KirimLIS> Members

		IEnumerator<KirimLIS> IEnumerable<KirimLIS>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as KirimLIS;
			}
		}

		#endregion
		
		private KirimLISQuery query;
	}


	/// <summary>
	/// Encapsulates the 'KirimLIS' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("KirimLIS ({Kode})")]
	[Serializable]
	public partial class KirimLIS : esKirimLIS
	{
		public KirimLIS()
		{

		}
	
		public KirimLIS(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return KirimLISMetadata.Meta();
			}
		}
		
		
		
		override protected esKirimLISQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KirimLISQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public KirimLISQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KirimLISQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(KirimLISQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private KirimLISQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class KirimLISQuery : esKirimLISQuery
	{
		public KirimLISQuery()
		{

		}		
		
		public KirimLISQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "KirimLISQuery";
        }
		
			
	}


	[Serializable]
	public partial class KirimLISMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected KirimLISMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Kode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Kode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.ModifiedDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISMetadata.PropertyNames.ModifiedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.NoPasien, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.NoPasien;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.KodeKunjungan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.KodeKunjungan;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Nama, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Nama;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Email, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.DateOfBirth, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISMetadata.PropertyNames.DateOfBirth;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.UmurTahun, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISMetadata.PropertyNames.UmurTahun;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.UmurBulan, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISMetadata.PropertyNames.UmurBulan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.UmurHari, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISMetadata.PropertyNames.UmurHari;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Gender, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Gender;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Alamat, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Alamat;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Diagnosa, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Diagnosa;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.TglPeriksa, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISMetadata.PropertyNames.TglPeriksa;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Pengirim, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Pengirim;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.PengirimName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.PengirimName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Kelas, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Kelas;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.KelasName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.KelasName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Ruang, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Ruang;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.RuangName, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.RuangName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.CaraBayar, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.CaraBayar;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.CaraBayarName, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.CaraBayarName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.KodeTarif, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.KodeTarif;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.ISInap, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.ISInap;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Status, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Status;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"(NULL)";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Update, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Update;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.NoReg, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.NoReg;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.FlagTaken, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISMetadata.PropertyNames.FlagTaken;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Nik, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Nik;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISMetadata.ColumnNames.Catatan, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISMetadata.PropertyNames.Catatan;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public KirimLISMetadata Meta()
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
			 public const string ModifiedDate = "modified_date";
			 public const string NoPasien = "No_Pasien";
			 public const string KodeKunjungan = "Kode_Kunjungan";
			 public const string Nama = "Nama";
			 public const string Email = "Email";
			 public const string DateOfBirth = "Date_of_birth";
			 public const string UmurTahun = "UmurTahun";
			 public const string UmurBulan = "UmurBulan";
			 public const string UmurHari = "UmurHari";
			 public const string Gender = "Gender";
			 public const string Alamat = "Alamat";
			 public const string Diagnosa = "Diagnosa";
			 public const string TglPeriksa = "Tgl_Periksa";
			 public const string Pengirim = "Pengirim";
			 public const string PengirimName = "pengirim_name";
			 public const string Kelas = "Kelas";
			 public const string KelasName = "kelas_name";
			 public const string Ruang = "Ruang";
			 public const string RuangName = "ruang_name";
			 public const string CaraBayar = "Cara_Bayar";
			 public const string CaraBayarName = "cara_bayar_name";
			 public const string KodeTarif = "Kode_Tarif";
			 public const string ISInap = "IS_Inap";
			 public const string Status = "Status";
			 public const string Update = "update";
			 public const string NoReg = "No_reg";
			 public const string FlagTaken = "FLAG_TAKEN";
			 public const string Nik = "NIK";
			 public const string Catatan = "CATATAN";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Kode = "Kode";
			 public const string ModifiedDate = "ModifiedDate";
			 public const string NoPasien = "NoPasien";
			 public const string KodeKunjungan = "KodeKunjungan";
			 public const string Nama = "Nama";
			 public const string Email = "Email";
			 public const string DateOfBirth = "DateOfBirth";
			 public const string UmurTahun = "UmurTahun";
			 public const string UmurBulan = "UmurBulan";
			 public const string UmurHari = "UmurHari";
			 public const string Gender = "Gender";
			 public const string Alamat = "Alamat";
			 public const string Diagnosa = "Diagnosa";
			 public const string TglPeriksa = "TglPeriksa";
			 public const string Pengirim = "Pengirim";
			 public const string PengirimName = "PengirimName";
			 public const string Kelas = "Kelas";
			 public const string KelasName = "KelasName";
			 public const string Ruang = "Ruang";
			 public const string RuangName = "RuangName";
			 public const string CaraBayar = "CaraBayar";
			 public const string CaraBayarName = "CaraBayarName";
			 public const string KodeTarif = "KodeTarif";
			 public const string ISInap = "ISInap";
			 public const string Status = "Status";
			 public const string Update = "Update";
			 public const string NoReg = "NoReg";
			 public const string FlagTaken = "FlagTaken";
			 public const string Nik = "Nik";
			 public const string Catatan = "Catatan";
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
			lock (typeof(KirimLISMetadata))
			{
				if(KirimLISMetadata.mapDelegates == null)
				{
					KirimLISMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (KirimLISMetadata.meta == null)
				{
					KirimLISMetadata.meta = new KirimLISMetadata();
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
				meta.AddTypeMap("ModifiedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("NoPasien", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeKunjungan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nama", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UmurTahun", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UmurBulan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UmurHari", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Gender", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Alamat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Diagnosa", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglPeriksa", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Pengirim", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PengirimName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kelas", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KelasName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ruang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RuangName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CaraBayar", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CaraBayarName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeTarif", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ISInap", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Status", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Update", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("NoReg", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FlagTaken", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Nik", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Catatan", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "KirimLIS";
				meta.Destination = "KirimLIS";
				
				meta.spInsert = "proc_KirimLISInsert";				
				meta.spUpdate = "proc_KirimLISUpdate";		
				meta.spDelete = "proc_KirimLISDelete";
				meta.spLoadAll = "proc_KirimLISLoadAll";
				meta.spLoadByPrimaryKey = "proc_KirimLISLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private KirimLISMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
