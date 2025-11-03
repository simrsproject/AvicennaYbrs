/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/9/2021 7:17:56 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.VANSLITE
{

	[Serializable]
	abstract public class esOrderLabCollection : esEntityCollectionWAuditLog
	{
		public esOrderLabCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OrderLabCollection";
		}

		#region Query Logic
		protected void InitQuery(esOrderLabQuery query)
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
			this.InitQuery(query as esOrderLabQuery);
		}
		#endregion
		
		virtual public OrderLab DetachEntity(OrderLab entity)
		{
			return base.DetachEntity(entity) as OrderLab;
		}
		
		virtual public OrderLab AttachEntity(OrderLab entity)
		{
			return base.AttachEntity(entity) as OrderLab;
		}
		
		virtual public void Combine(OrderLabCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OrderLab this[int index]
		{
			get
			{
				return base[index] as OrderLab;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OrderLab);
		}
	}



	[Serializable]
	abstract public class esOrderLab : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOrderLabQuery GetDynamicQuery()
		{
			return null;
		}

		public esOrderLab()
		{

		}

		public esOrderLab(DataRow row)
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
			esOrderLabQuery query = this.GetDynamicQuery();
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
						case "AsalLab": this.str.AsalLab = (string)value; break;							
						case "NoLab": this.str.NoLab = (string)value; break;							
						case "NoLabDtl": this.str.NoLabDtl = (string)value; break;							
						case "NoRegistrasi": this.str.NoRegistrasi = (string)value; break;							
						case "NoRm": this.str.NoRm = (string)value; break;							
						case "TglOrder": this.str.TglOrder = (string)value; break;							
						case "NamaPas": this.str.NamaPas = (string)value; break;							
						case "JenisKel": this.str.JenisKel = (string)value; break;							
						case "TglLahir": this.str.TglLahir = (string)value; break;							
						case "Usia": this.str.Usia = (string)value; break;							
						case "Alamat": this.str.Alamat = (string)value; break;							
						case "KodeDokKirim": this.str.KodeDokKirim = (string)value; break;							
						case "NamaDokKirim": this.str.NamaDokKirim = (string)value; break;							
						case "KodeRuang": this.str.KodeRuang = (string)value; break;							
						case "NamaRuang": this.str.NamaRuang = (string)value; break;							
						case "KodeCaraBayar": this.str.KodeCaraBayar = (string)value; break;							
						case "CaraBayar": this.str.CaraBayar = (string)value; break;							
						case "KetKlinis": this.str.KetKlinis = (string)value; break;							
						case "KodeTest": this.str.KodeTest = (string)value; break;							
						case "Test": this.str.Test = (string)value; break;							
						case "Harga": this.str.Harga = (string)value; break;							
						case "WaktuKirim": this.str.WaktuKirim = (string)value; break;							
						case "Prioritas": this.str.Prioritas = (string)value; break;							
						case "JnsRawat": this.str.JnsRawat = (string)value; break;							
						case "DokJaga": this.str.DokJaga = (string)value; break;							
						case "Status": this.str.Status = (string)value; break;							
						case "Batal": this.str.Batal = (string)value; break;							
						case "JumlahTest": this.str.JumlahTest = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TglOrder":
						
							if (value == null || value is System.DateTime)
								this.TglOrder = (System.DateTime?)value;
							break;
						
						case "TglLahir":
						
							if (value == null || value is System.DateTime)
								this.TglLahir = (System.DateTime?)value;
							break;
						
						case "Harga":
						
							if (value == null || value is System.Int32)
								this.Harga = (System.Int32?)value;
							break;
						
						case "WaktuKirim":
						
							if (value == null || value is System.DateTime)
								this.WaktuKirim = (System.DateTime?)value;
							break;
						
						case "Status":
						
							if (value == null || value is System.Byte)
								this.Status = (System.Byte?)value;
							break;
						
						case "Batal":
						
							if (value == null || value is System.Byte)
								this.Batal = (System.Byte?)value;
							break;
						
						case "JumlahTest":
						
							if (value == null || value is System.Int32)
								this.JumlahTest = (System.Int32?)value;
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
		/// Maps to order_lab.asal_lab
		/// </summary>
		virtual public System.String AsalLab
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.AsalLab);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.AsalLab, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.no_lab
		/// </summary>
		virtual public System.String NoLab
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NoLab);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NoLab, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.no_lab_dtl
		/// </summary>
		virtual public System.String NoLabDtl
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NoLabDtl);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NoLabDtl, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.no_registrasi
		/// </summary>
		virtual public System.String NoRegistrasi
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NoRegistrasi);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NoRegistrasi, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.no_rm
		/// </summary>
		virtual public System.String NoRm
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NoRm);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NoRm, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.tgl_order
		/// </summary>
		virtual public System.DateTime? TglOrder
		{
			get
			{
				return base.GetSystemDateTime(OrderLabMetadata.ColumnNames.TglOrder);
			}
			
			set
			{
				base.SetSystemDateTime(OrderLabMetadata.ColumnNames.TglOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.nama_pas
		/// </summary>
		virtual public System.String NamaPas
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NamaPas);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NamaPas, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.jenis_kel
		/// </summary>
		virtual public System.String JenisKel
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.JenisKel);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.JenisKel, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.tgl_lahir
		/// </summary>
		virtual public System.DateTime? TglLahir
		{
			get
			{
				return base.GetSystemDateTime(OrderLabMetadata.ColumnNames.TglLahir);
			}
			
			set
			{
				base.SetSystemDateTime(OrderLabMetadata.ColumnNames.TglLahir, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.usia
		/// </summary>
		virtual public System.String Usia
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.Usia);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.Usia, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.alamat
		/// </summary>
		virtual public System.String Alamat
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.Alamat);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.Alamat, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.kode_dok_kirim
		/// </summary>
		virtual public System.String KodeDokKirim
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.KodeDokKirim);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.KodeDokKirim, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.nama_dok_kirim
		/// </summary>
		virtual public System.String NamaDokKirim
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NamaDokKirim);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NamaDokKirim, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.kode_ruang
		/// </summary>
		virtual public System.String KodeRuang
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.KodeRuang);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.KodeRuang, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.nama_ruang
		/// </summary>
		virtual public System.String NamaRuang
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.NamaRuang);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.NamaRuang, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.kode_cara_bayar
		/// </summary>
		virtual public System.String KodeCaraBayar
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.KodeCaraBayar);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.KodeCaraBayar, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.cara_bayar
		/// </summary>
		virtual public System.String CaraBayar
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.CaraBayar);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.CaraBayar, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.ket_klinis
		/// </summary>
		virtual public System.String KetKlinis
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.KetKlinis);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.KetKlinis, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.kode_test
		/// </summary>
		virtual public System.String KodeTest
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.KodeTest);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.KodeTest, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.test
		/// </summary>
		virtual public System.String Test
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.Test);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.Test, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.Harga
		/// </summary>
		virtual public System.Int32? Harga
		{
			get
			{
				return base.GetSystemInt32(OrderLabMetadata.ColumnNames.Harga);
			}
			
			set
			{
				base.SetSystemInt32(OrderLabMetadata.ColumnNames.Harga, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.waktu_kirim
		/// </summary>
		virtual public System.DateTime? WaktuKirim
		{
			get
			{
				return base.GetSystemDateTime(OrderLabMetadata.ColumnNames.WaktuKirim);
			}
			
			set
			{
				base.SetSystemDateTime(OrderLabMetadata.ColumnNames.WaktuKirim, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.prioritas
		/// </summary>
		virtual public System.String Prioritas
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.Prioritas);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.Prioritas, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.jns_rawat
		/// </summary>
		virtual public System.String JnsRawat
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.JnsRawat);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.JnsRawat, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.dok_jaga
		/// </summary>
		virtual public System.String DokJaga
		{
			get
			{
				return base.GetSystemString(OrderLabMetadata.ColumnNames.DokJaga);
			}
			
			set
			{
				base.SetSystemString(OrderLabMetadata.ColumnNames.DokJaga, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.status
		/// </summary>
		virtual public System.Byte? Status
		{
			get
			{
				return base.GetSystemByte(OrderLabMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemByte(OrderLabMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.Batal
		/// </summary>
		virtual public System.Byte? Batal
		{
			get
			{
				return base.GetSystemByte(OrderLabMetadata.ColumnNames.Batal);
			}
			
			set
			{
				base.SetSystemByte(OrderLabMetadata.ColumnNames.Batal, value);
			}
		}
		
		/// <summary>
		/// Maps to order_lab.Jumlah_test
		/// </summary>
		virtual public System.Int32? JumlahTest
		{
			get
			{
				return base.GetSystemInt32(OrderLabMetadata.ColumnNames.JumlahTest);
			}
			
			set
			{
				base.SetSystemInt32(OrderLabMetadata.ColumnNames.JumlahTest, value);
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
			public esStrings(esOrderLab entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AsalLab
			{
				get
				{
					System.String data = entity.AsalLab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AsalLab = null;
					else entity.AsalLab = Convert.ToString(value);
				}
			}
				
			public System.String NoLab
			{
				get
				{
					System.String data = entity.NoLab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoLab = null;
					else entity.NoLab = Convert.ToString(value);
				}
			}
				
			public System.String NoLabDtl
			{
				get
				{
					System.String data = entity.NoLabDtl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoLabDtl = null;
					else entity.NoLabDtl = Convert.ToString(value);
				}
			}
				
			public System.String NoRegistrasi
			{
				get
				{
					System.String data = entity.NoRegistrasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoRegistrasi = null;
					else entity.NoRegistrasi = Convert.ToString(value);
				}
			}
				
			public System.String NoRm
			{
				get
				{
					System.String data = entity.NoRm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoRm = null;
					else entity.NoRm = Convert.ToString(value);
				}
			}
				
			public System.String TglOrder
			{
				get
				{
					System.DateTime? data = entity.TglOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglOrder = null;
					else entity.TglOrder = Convert.ToDateTime(value);
				}
			}
				
			public System.String NamaPas
			{
				get
				{
					System.String data = entity.NamaPas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaPas = null;
					else entity.NamaPas = Convert.ToString(value);
				}
			}
				
			public System.String JenisKel
			{
				get
				{
					System.String data = entity.JenisKel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisKel = null;
					else entity.JenisKel = Convert.ToString(value);
				}
			}
				
			public System.String TglLahir
			{
				get
				{
					System.DateTime? data = entity.TglLahir;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglLahir = null;
					else entity.TglLahir = Convert.ToDateTime(value);
				}
			}
				
			public System.String Usia
			{
				get
				{
					System.String data = entity.Usia;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Usia = null;
					else entity.Usia = Convert.ToString(value);
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
				
			public System.String KodeDokKirim
			{
				get
				{
					System.String data = entity.KodeDokKirim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeDokKirim = null;
					else entity.KodeDokKirim = Convert.ToString(value);
				}
			}
				
			public System.String NamaDokKirim
			{
				get
				{
					System.String data = entity.NamaDokKirim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaDokKirim = null;
					else entity.NamaDokKirim = Convert.ToString(value);
				}
			}
				
			public System.String KodeRuang
			{
				get
				{
					System.String data = entity.KodeRuang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeRuang = null;
					else entity.KodeRuang = Convert.ToString(value);
				}
			}
				
			public System.String NamaRuang
			{
				get
				{
					System.String data = entity.NamaRuang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaRuang = null;
					else entity.NamaRuang = Convert.ToString(value);
				}
			}
				
			public System.String KodeCaraBayar
			{
				get
				{
					System.String data = entity.KodeCaraBayar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeCaraBayar = null;
					else entity.KodeCaraBayar = Convert.ToString(value);
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
				
			public System.String KetKlinis
			{
				get
				{
					System.String data = entity.KetKlinis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KetKlinis = null;
					else entity.KetKlinis = Convert.ToString(value);
				}
			}
				
			public System.String KodeTest
			{
				get
				{
					System.String data = entity.KodeTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeTest = null;
					else entity.KodeTest = Convert.ToString(value);
				}
			}
				
			public System.String Test
			{
				get
				{
					System.String data = entity.Test;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Test = null;
					else entity.Test = Convert.ToString(value);
				}
			}
				
			public System.String Harga
			{
				get
				{
					System.Int32? data = entity.Harga;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Harga = null;
					else entity.Harga = Convert.ToInt32(value);
				}
			}
				
			public System.String WaktuKirim
			{
				get
				{
					System.DateTime? data = entity.WaktuKirim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WaktuKirim = null;
					else entity.WaktuKirim = Convert.ToDateTime(value);
				}
			}
				
			public System.String Prioritas
			{
				get
				{
					System.String data = entity.Prioritas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Prioritas = null;
					else entity.Prioritas = Convert.ToString(value);
				}
			}
				
			public System.String JnsRawat
			{
				get
				{
					System.String data = entity.JnsRawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JnsRawat = null;
					else entity.JnsRawat = Convert.ToString(value);
				}
			}
				
			public System.String DokJaga
			{
				get
				{
					System.String data = entity.DokJaga;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DokJaga = null;
					else entity.DokJaga = Convert.ToString(value);
				}
			}
				
			public System.String Status
			{
				get
				{
					System.Byte? data = entity.Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Status = null;
					else entity.Status = Convert.ToByte(value);
				}
			}
				
			public System.String Batal
			{
				get
				{
					System.Byte? data = entity.Batal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Batal = null;
					else entity.Batal = Convert.ToByte(value);
				}
			}
				
			public System.String JumlahTest
			{
				get
				{
					System.Int32? data = entity.JumlahTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahTest = null;
					else entity.JumlahTest = Convert.ToInt32(value);
				}
			}
			

			private esOrderLab entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOrderLabQuery query)
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
				throw new Exception("esOrderLab can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esOrderLabQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderLabMetadata.Meta();
			}
		}	
		

		public esQueryItem AsalLab
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.AsalLab, esSystemType.String);
			}
		} 
		
		public esQueryItem NoLab
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NoLab, esSystemType.String);
			}
		} 
		
		public esQueryItem NoLabDtl
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NoLabDtl, esSystemType.String);
			}
		} 
		
		public esQueryItem NoRegistrasi
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NoRegistrasi, esSystemType.String);
			}
		} 
		
		public esQueryItem NoRm
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NoRm, esSystemType.String);
			}
		} 
		
		public esQueryItem TglOrder
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.TglOrder, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NamaPas
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NamaPas, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisKel
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.JenisKel, esSystemType.String);
			}
		} 
		
		public esQueryItem TglLahir
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.TglLahir, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Usia
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Usia, esSystemType.String);
			}
		} 
		
		public esQueryItem Alamat
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Alamat, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeDokKirim
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.KodeDokKirim, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaDokKirim
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NamaDokKirim, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeRuang
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.KodeRuang, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaRuang
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.NamaRuang, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeCaraBayar
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.KodeCaraBayar, esSystemType.String);
			}
		} 
		
		public esQueryItem CaraBayar
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.CaraBayar, esSystemType.String);
			}
		} 
		
		public esQueryItem KetKlinis
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.KetKlinis, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTest
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.KodeTest, esSystemType.String);
			}
		} 
		
		public esQueryItem Test
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Test, esSystemType.String);
			}
		} 
		
		public esQueryItem Harga
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Harga, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WaktuKirim
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.WaktuKirim, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Prioritas
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Prioritas, esSystemType.String);
			}
		} 
		
		public esQueryItem JnsRawat
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.JnsRawat, esSystemType.String);
			}
		} 
		
		public esQueryItem DokJaga
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.DokJaga, esSystemType.String);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Status, esSystemType.Byte);
			}
		} 
		
		public esQueryItem Batal
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.Batal, esSystemType.Byte);
			}
		} 
		
		public esQueryItem JumlahTest
		{
			get
			{
				return new esQueryItem(this, OrderLabMetadata.ColumnNames.JumlahTest, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OrderLabCollection")]
	public partial class OrderLabCollection : esOrderLabCollection, IEnumerable<OrderLab>
	{
		public OrderLabCollection()
		{

		}
		
		public static implicit operator List<OrderLab>(OrderLabCollection coll)
		{
			List<OrderLab> list = new List<OrderLab>();
			
			foreach (OrderLab emp in coll)
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
				return  OrderLabMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderLabQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OrderLab(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OrderLab();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OrderLabQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderLabQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OrderLabQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OrderLab AddNew()
		{
			OrderLab entity = base.AddNewEntity() as OrderLab;
			
			return entity;
		}

		public OrderLab FindByPrimaryKey()
		{
			return base.FindByPrimaryKey() as OrderLab;
		}


		#region IEnumerable<OrderLab> Members

		IEnumerator<OrderLab> IEnumerable<OrderLab>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OrderLab;
			}
		}

		#endregion
		
		private OrderLabQuery query;
	}


	/// <summary>
	/// Encapsulates the 'order_lab' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OrderLab ()")]
	[Serializable]
	public partial class OrderLab : esOrderLab
	{
		public OrderLab()
		{

		}
	
		public OrderLab(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderLabMetadata.Meta();
			}
		}
		
		
		
		override protected esOrderLabQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderLabQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OrderLabQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderLabQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OrderLabQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OrderLabQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OrderLabQuery : esOrderLabQuery
	{
		public OrderLabQuery()
		{

		}		
		
		public OrderLabQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OrderLabQuery";
        }
		
			
	}


	[Serializable]
	public partial class OrderLabMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderLabMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.AsalLab, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.AsalLab;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NoLab, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NoLab;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NoLabDtl, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NoLabDtl;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NoRegistrasi, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NoRegistrasi;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NoRm, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NoRm;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.TglOrder, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderLabMetadata.PropertyNames.TglOrder;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NamaPas, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NamaPas;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.JenisKel, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.JenisKel;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.TglLahir, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderLabMetadata.PropertyNames.TglLahir;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Usia, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.Usia;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Alamat, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.Alamat;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.KodeDokKirim, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.KodeDokKirim;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NamaDokKirim, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NamaDokKirim;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.KodeRuang, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.KodeRuang;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.NamaRuang, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.NamaRuang;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.KodeCaraBayar, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.KodeCaraBayar;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.CaraBayar, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.CaraBayar;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.KetKlinis, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.KetKlinis;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.KodeTest, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.KodeTest;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Test, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.Test;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Harga, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderLabMetadata.PropertyNames.Harga;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.WaktuKirim, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderLabMetadata.PropertyNames.WaktuKirim;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Prioritas, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.Prioritas;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.JnsRawat, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.JnsRawat;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.DokJaga, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabMetadata.PropertyNames.DokJaga;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Status, 25, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = OrderLabMetadata.PropertyNames.Status;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.Batal, 26, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = OrderLabMetadata.PropertyNames.Batal;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabMetadata.ColumnNames.JumlahTest, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderLabMetadata.PropertyNames.JumlahTest;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderLabMetadata Meta()
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
			 public const string AsalLab = "asal_lab";
			 public const string NoLab = "no_lab";
			 public const string NoLabDtl = "no_lab_dtl";
			 public const string NoRegistrasi = "no_registrasi";
			 public const string NoRm = "no_rm";
			 public const string TglOrder = "tgl_order";
			 public const string NamaPas = "nama_pas";
			 public const string JenisKel = "jenis_kel";
			 public const string TglLahir = "tgl_lahir";
			 public const string Usia = "usia";
			 public const string Alamat = "alamat";
			 public const string KodeDokKirim = "kode_dok_kirim";
			 public const string NamaDokKirim = "nama_dok_kirim";
			 public const string KodeRuang = "kode_ruang";
			 public const string NamaRuang = "nama_ruang";
			 public const string KodeCaraBayar = "kode_cara_bayar";
			 public const string CaraBayar = "cara_bayar";
			 public const string KetKlinis = "ket_klinis";
			 public const string KodeTest = "kode_test";
			 public const string Test = "test";
			 public const string Harga = "Harga";
			 public const string WaktuKirim = "waktu_kirim";
			 public const string Prioritas = "prioritas";
			 public const string JnsRawat = "jns_rawat";
			 public const string DokJaga = "dok_jaga";
			 public const string Status = "status";
			 public const string Batal = "Batal";
			 public const string JumlahTest = "Jumlah_test";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AsalLab = "AsalLab";
			 public const string NoLab = "NoLab";
			 public const string NoLabDtl = "NoLabDtl";
			 public const string NoRegistrasi = "NoRegistrasi";
			 public const string NoRm = "NoRm";
			 public const string TglOrder = "TglOrder";
			 public const string NamaPas = "NamaPas";
			 public const string JenisKel = "JenisKel";
			 public const string TglLahir = "TglLahir";
			 public const string Usia = "Usia";
			 public const string Alamat = "Alamat";
			 public const string KodeDokKirim = "KodeDokKirim";
			 public const string NamaDokKirim = "NamaDokKirim";
			 public const string KodeRuang = "KodeRuang";
			 public const string NamaRuang = "NamaRuang";
			 public const string KodeCaraBayar = "KodeCaraBayar";
			 public const string CaraBayar = "CaraBayar";
			 public const string KetKlinis = "KetKlinis";
			 public const string KodeTest = "KodeTest";
			 public const string Test = "Test";
			 public const string Harga = "Harga";
			 public const string WaktuKirim = "WaktuKirim";
			 public const string Prioritas = "Prioritas";
			 public const string JnsRawat = "JnsRawat";
			 public const string DokJaga = "DokJaga";
			 public const string Status = "Status";
			 public const string Batal = "Batal";
			 public const string JumlahTest = "JumlahTest";
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
			lock (typeof(OrderLabMetadata))
			{
				if(OrderLabMetadata.mapDelegates == null)
				{
					OrderLabMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderLabMetadata.meta == null)
				{
					OrderLabMetadata.meta = new OrderLabMetadata();
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
				

				meta.AddTypeMap("AsalLab", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoLab", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoLabDtl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoRegistrasi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoRm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglOrder", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("NamaPas", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisKel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglLahir", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Usia", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Alamat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeDokKirim", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaDokKirim", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeRuang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaRuang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeCaraBayar", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CaraBayar", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KetKlinis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeTest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Test", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Harga", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WaktuKirim", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Prioritas", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JnsRawat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DokJaga", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Status", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("Batal", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("JumlahTest", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "order_lab";
				meta.Destination = "order_lab";
				
				meta.spInsert = "proc_order_labInsert";				
				meta.spUpdate = "proc_order_labUpdate";		
				meta.spDelete = "proc_order_labDelete";
				meta.spLoadAll = "proc_order_labLoadAll";
				meta.spLoadByPrimaryKey = "proc_order_labLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderLabMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
