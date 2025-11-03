/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/16/2021 6:40:36 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.ELIMS
{

	[Serializable]
	abstract public class esKirimLISTEMPCollection : esEntityCollectionWAuditLog
	{
		public esKirimLISTEMPCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "KirimLISTEMPCollection";
		}

		#region Query Logic
		protected void InitQuery(esKirimLISTEMPQuery query)
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
			this.InitQuery(query as esKirimLISTEMPQuery);
		}
		#endregion
		
		virtual public KirimLISTEMP DetachEntity(KirimLISTEMP entity)
		{
			return base.DetachEntity(entity) as KirimLISTEMP;
		}
		
		virtual public KirimLISTEMP AttachEntity(KirimLISTEMP entity)
		{
			return base.AttachEntity(entity) as KirimLISTEMP;
		}
		
		virtual public void Combine(KirimLISTEMPCollection collection)
		{
			base.Combine(collection);
		}
		
		new public KirimLISTEMP this[int index]
		{
			get
			{
				return base[index] as KirimLISTEMP;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(KirimLISTEMP);
		}
	}



	[Serializable]
	abstract public class esKirimLISTEMP : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esKirimLISTEMPQuery GetDynamicQuery()
		{
			return null;
		}

		public esKirimLISTEMP()
		{

		}

		public esKirimLISTEMP(DataRow row)
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
			esKirimLISTEMPQuery query = this.GetDynamicQuery();
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
		/// Maps to KirimLIS_TEMP.kode
		/// </summary>
		virtual public System.String Kode
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Kode);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Kode, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.modified_date
		/// </summary>
		virtual public System.DateTime? ModifiedDate
		{
			get
			{
				return base.GetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.ModifiedDate);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.ModifiedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.No_Pasien
		/// </summary>
		virtual public System.String NoPasien
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.NoPasien);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.NoPasien, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Kode_Kunjungan
		/// </summary>
		virtual public System.String KodeKunjungan
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.KodeKunjungan);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.KodeKunjungan, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Nama
		/// </summary>
		virtual public System.String Nama
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Nama);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Nama, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Date_of_birth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.DateOfBirth);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.UmurTahun
		/// </summary>
		virtual public System.Int32? UmurTahun
		{
			get
			{
				return base.GetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurTahun);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurTahun, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.UmurBulan
		/// </summary>
		virtual public System.Int32? UmurBulan
		{
			get
			{
				return base.GetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurBulan);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurBulan, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.UmurHari
		/// </summary>
		virtual public System.Int32? UmurHari
		{
			get
			{
				return base.GetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurHari);
			}
			
			set
			{
				base.SetSystemInt32(KirimLISTEMPMetadata.ColumnNames.UmurHari, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Gender, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Alamat
		/// </summary>
		virtual public System.String Alamat
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Alamat);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Alamat, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Diagnosa
		/// </summary>
		virtual public System.String Diagnosa
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Diagnosa);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Diagnosa, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Tgl_Periksa
		/// </summary>
		virtual public System.DateTime? TglPeriksa
		{
			get
			{
				return base.GetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.TglPeriksa);
			}
			
			set
			{
				base.SetSystemDateTime(KirimLISTEMPMetadata.ColumnNames.TglPeriksa, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Pengirim
		/// </summary>
		virtual public System.String Pengirim
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Pengirim);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Pengirim, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.pengirim_name
		/// </summary>
		virtual public System.String PengirimName
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.PengirimName);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.PengirimName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Kelas
		/// </summary>
		virtual public System.String Kelas
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Kelas);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Kelas, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.kelas_name
		/// </summary>
		virtual public System.String KelasName
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.KelasName);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.KelasName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Ruang
		/// </summary>
		virtual public System.String Ruang
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Ruang);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Ruang, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.ruang_name
		/// </summary>
		virtual public System.String RuangName
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.RuangName);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.RuangName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Cara_Bayar
		/// </summary>
		virtual public System.String CaraBayar
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.CaraBayar);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.CaraBayar, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.cara_bayar_name
		/// </summary>
		virtual public System.String CaraBayarName
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.CaraBayarName);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.CaraBayarName, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Kode_Tarif
		/// </summary>
		virtual public System.String KodeTarif
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.KodeTarif);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.KodeTarif, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.IS_Inap
		/// </summary>
		virtual public System.String ISInap
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.ISInap);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.ISInap, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.Status
		/// </summary>
		virtual public System.String Status
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.update
		/// </summary>
		virtual public System.String Update
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Update);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Update, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.NIK
		/// </summary>
		virtual public System.String Nik
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Nik);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Nik, value);
			}
		}
		
		/// <summary>
		/// Maps to KirimLIS_TEMP.CATATAN
		/// </summary>
		virtual public System.String Catatan
		{
			get
			{
				return base.GetSystemString(KirimLISTEMPMetadata.ColumnNames.Catatan);
			}
			
			set
			{
				base.SetSystemString(KirimLISTEMPMetadata.ColumnNames.Catatan, value);
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
			public esStrings(esKirimLISTEMP entity)
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
			

			private esKirimLISTEMP entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esKirimLISTEMPQuery query)
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
				throw new Exception("esKirimLISTEMP can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esKirimLISTEMPQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return KirimLISTEMPMetadata.Meta();
			}
		}	
		

		public esQueryItem Kode
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Kode, esSystemType.String);
			}
		} 
		
		public esQueryItem ModifiedDate
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.ModifiedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoPasien
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.NoPasien, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeKunjungan
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.KodeKunjungan, esSystemType.String);
			}
		} 
		
		public esQueryItem Nama
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Nama, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem UmurTahun
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.UmurTahun, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UmurBulan
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.UmurBulan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UmurHari
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.UmurHari, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
		
		public esQueryItem Alamat
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Alamat, esSystemType.String);
			}
		} 
		
		public esQueryItem Diagnosa
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Diagnosa, esSystemType.String);
			}
		} 
		
		public esQueryItem TglPeriksa
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.TglPeriksa, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Pengirim
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Pengirim, esSystemType.String);
			}
		} 
		
		public esQueryItem PengirimName
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.PengirimName, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelas
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Kelas, esSystemType.String);
			}
		} 
		
		public esQueryItem KelasName
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.KelasName, esSystemType.String);
			}
		} 
		
		public esQueryItem Ruang
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Ruang, esSystemType.String);
			}
		} 
		
		public esQueryItem RuangName
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.RuangName, esSystemType.String);
			}
		} 
		
		public esQueryItem CaraBayar
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.CaraBayar, esSystemType.String);
			}
		} 
		
		public esQueryItem CaraBayarName
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.CaraBayarName, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTarif
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.KodeTarif, esSystemType.String);
			}
		} 
		
		public esQueryItem ISInap
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.ISInap, esSystemType.String);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Status, esSystemType.String);
			}
		} 
		
		public esQueryItem Update
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Update, esSystemType.String);
			}
		} 
		
		public esQueryItem Nik
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Nik, esSystemType.String);
			}
		} 
		
		public esQueryItem Catatan
		{
			get
			{
				return new esQueryItem(this, KirimLISTEMPMetadata.ColumnNames.Catatan, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("KirimLISTEMPCollection")]
	public partial class KirimLISTEMPCollection : esKirimLISTEMPCollection, IEnumerable<KirimLISTEMP>
	{
		public KirimLISTEMPCollection()
		{

		}
		
		public static implicit operator List<KirimLISTEMP>(KirimLISTEMPCollection coll)
		{
			List<KirimLISTEMP> list = new List<KirimLISTEMP>();
			
			foreach (KirimLISTEMP emp in coll)
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
				return  KirimLISTEMPMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KirimLISTEMPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new KirimLISTEMP(row);
		}

		override protected esEntity CreateEntity()
		{
			return new KirimLISTEMP();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public KirimLISTEMPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KirimLISTEMPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(KirimLISTEMPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public KirimLISTEMP AddNew()
		{
			KirimLISTEMP entity = base.AddNewEntity() as KirimLISTEMP;
			
			return entity;
		}

		public KirimLISTEMP FindByPrimaryKey(System.String kode)
		{
			return base.FindByPrimaryKey(kode) as KirimLISTEMP;
		}


		#region IEnumerable<KirimLISTEMP> Members

		IEnumerator<KirimLISTEMP> IEnumerable<KirimLISTEMP>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as KirimLISTEMP;
			}
		}

		#endregion
		
		private KirimLISTEMPQuery query;
	}


	/// <summary>
	/// Encapsulates the 'KirimLIS_TEMP' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("KirimLISTEMP ({Kode})")]
	[Serializable]
	public partial class KirimLISTEMP : esKirimLISTEMP
	{
		public KirimLISTEMP()
		{

		}
	
		public KirimLISTEMP(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return KirimLISTEMPMetadata.Meta();
			}
		}
		
		
		
		override protected esKirimLISTEMPQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KirimLISTEMPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public KirimLISTEMPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KirimLISTEMPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(KirimLISTEMPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private KirimLISTEMPQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class KirimLISTEMPQuery : esKirimLISTEMPQuery
	{
		public KirimLISTEMPQuery()
		{

		}		
		
		public KirimLISTEMPQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "KirimLISTEMPQuery";
        }
		
			
	}


	[Serializable]
	public partial class KirimLISTEMPMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected KirimLISTEMPMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Kode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Kode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.ModifiedDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.ModifiedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.NoPasien, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.NoPasien;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.KodeKunjungan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.KodeKunjungan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Nama, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Nama;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Email, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.DateOfBirth, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.DateOfBirth;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.UmurTahun, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.UmurTahun;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.UmurBulan, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.UmurBulan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.UmurHari, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.UmurHari;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Gender, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Gender;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Alamat, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Alamat;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Diagnosa, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Diagnosa;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.TglPeriksa, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.TglPeriksa;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Pengirim, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Pengirim;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.PengirimName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.PengirimName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Kelas, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Kelas;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.KelasName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.KelasName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Ruang, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Ruang;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.RuangName, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.RuangName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.CaraBayar, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.CaraBayar;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.CaraBayarName, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.CaraBayarName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.KodeTarif, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.KodeTarif;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.ISInap, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.ISInap;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Status, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Status;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Update, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Update;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Nik, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Nik;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(KirimLISTEMPMetadata.ColumnNames.Catatan, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = KirimLISTEMPMetadata.PropertyNames.Catatan;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public KirimLISTEMPMetadata Meta()
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
			lock (typeof(KirimLISTEMPMetadata))
			{
				if(KirimLISTEMPMetadata.mapDelegates == null)
				{
					KirimLISTEMPMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (KirimLISTEMPMetadata.meta == null)
				{
					KirimLISTEMPMetadata.meta = new KirimLISTEMPMetadata();
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
				meta.AddTypeMap("Nik", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Catatan", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "KirimLIS_TEMP";
				meta.Destination = "KirimLIS_TEMP";
				
				meta.spInsert = "proc_KirimLIS_TEMPInsert";				
				meta.spUpdate = "proc_KirimLIS_TEMPUpdate";		
				meta.spDelete = "proc_KirimLIS_TEMPDelete";
				meta.spLoadAll = "proc_KirimLIS_TEMPLoadAll";
				meta.spLoadByPrimaryKey = "proc_KirimLIS_TEMPLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private KirimLISTEMPMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
