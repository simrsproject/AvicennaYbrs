/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/24/2018 3:45:46 AM
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



namespace Temiang.Avicenna.BusinessObject.Interop.VANSLAB
{

	[Serializable]
	abstract public class esLabHasilCollection : esEntityCollectionWAuditLog
	{
		public esLabHasilCollection()
		{

		}
		protected override string GetCollectionName()
		{
			return "LabHasilCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabHasilQuery query)
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
			this.InitQuery(query as esLabHasilQuery);
		}
		#endregion
		
		virtual public LabHasil DetachEntity(LabHasil entity)
		{
			return base.DetachEntity(entity) as LabHasil;
		}
		
		virtual public LabHasil AttachEntity(LabHasil entity)
		{
			return base.AttachEntity(entity) as LabHasil;
		}
		
		virtual public void Combine(LabHasilCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LabHasil this[int index]
		{
			get
			{
				return base[index] as LabHasil;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LabHasil);
		}
	}



	[Serializable]
	abstract public class esLabHasil : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabHasilQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabHasil()
		{

		}

		public esLabHasil(DataRow row)
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
        //    if (sqlAccessType == esSqlAccessType.DynamicSQL)
        //        return LoadByPrimaryKeyDynamic();
        //    else
        //        return LoadByPrimaryKeyStoredProcedure();
        //}

		private bool LoadByPrimaryKeyDynamic()
		{
			esLabHasilQuery query = this.GetDynamicQuery();
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
						case "NoPesanan": this.str.NoPesanan = (string)value; break;							
						case "NoLab": this.str.NoLab = (string)value; break;							
						case "NoRegistrasi": this.str.NoRegistrasi = (string)value; break;							
						case "NoKunjungan": this.str.NoKunjungan = (string)value; break;							
						case "KodeSir": this.str.KodeSir = (string)value; break;							
						case "NoUrut": this.str.NoUrut = (string)value; break;							
						case "KodePemeriksaan": this.str.KodePemeriksaan = (string)value; break;							
						case "NamaPemeriksaan": this.str.NamaPemeriksaan = (string)value; break;							
						case "Unit": this.str.Unit = (string)value; break;							
						case "Normal": this.str.Normal = (string)value; break;							
						case "Hasil": this.str.Hasil = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;							
						case "FlagInsert": this.str.FlagInsert = (string)value; break;							
						case "TglJamInsert": this.str.TglJamInsert = (string)value; break;							
						case "FlagAmbil": this.str.FlagAmbil = (string)value; break;							
						case "TglJamAmbil": this.str.TglJamAmbil = (string)value; break;							
						case "Type": this.str.Type = (string)value; break;							
						case "NoRM": this.str.NoRM = (string)value; break;							
						case "TglDaftar": this.str.TglDaftar = (string)value; break;							
						case "TglHasil": this.str.TglHasil = (string)value; break;							
						case "KodeRuang": this.str.KodeRuang = (string)value; break;							
						case "KodeDokter": this.str.KodeDokter = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "NoUrut":
						
							if (value == null || value is System.Int32)
								this.NoUrut = (System.Int32?)value;
							break;
						
						case "TglJamInsert":
						
							if (value == null || value is System.DateTime)
								this.TglJamInsert = (System.DateTime?)value;
							break;
						
						case "TglJamAmbil":
						
							if (value == null || value is System.DateTime)
								this.TglJamAmbil = (System.DateTime?)value;
							break;
						
						case "TglDaftar":
						
							if (value == null || value is System.DateTime)
								this.TglDaftar = (System.DateTime?)value;
							break;
						
						case "TglHasil":
						
							if (value == null || value is System.DateTime)
								this.TglHasil = (System.DateTime?)value;
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
		/// Maps to lab_hasil.no_pesanan
		/// </summary>
		virtual public System.String NoPesanan
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NoPesanan);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NoPesanan, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.no_lab
		/// </summary>
		virtual public System.String NoLab
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NoLab);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NoLab, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.no_registrasi
		/// </summary>
		virtual public System.String NoRegistrasi
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NoRegistrasi);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NoRegistrasi, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.no_kunjungan
		/// </summary>
		virtual public System.String NoKunjungan
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NoKunjungan);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NoKunjungan, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.kode_sir
		/// </summary>
		virtual public System.String KodeSir
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.KodeSir);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.KodeSir, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.no_urut
		/// </summary>
		virtual public System.Int32? NoUrut
		{
			get
			{
				return base.GetSystemInt32(LabHasilMetadata.ColumnNames.NoUrut);
			}
			
			set
			{
				base.SetSystemInt32(LabHasilMetadata.ColumnNames.NoUrut, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.kode_pemeriksaan
		/// </summary>
		virtual public System.String KodePemeriksaan
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.KodePemeriksaan);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.KodePemeriksaan, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.nama_pemeriksaan
		/// </summary>
		virtual public System.String NamaPemeriksaan
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NamaPemeriksaan);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NamaPemeriksaan, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.unit
		/// </summary>
		virtual public System.String Unit
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.Unit);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.Unit, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.normal
		/// </summary>
		virtual public System.String Normal
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.Normal);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.Normal, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.hasil
		/// </summary>
		virtual public System.String Hasil
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.Hasil);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.Hasil, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.flag
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.Flag, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.flag_insert
		/// </summary>
		virtual public System.String FlagInsert
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.FlagInsert);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.FlagInsert, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.tgl_jam_insert
		/// </summary>
		virtual public System.DateTime? TglJamInsert
		{
			get
			{
				return base.GetSystemDateTime(LabHasilMetadata.ColumnNames.TglJamInsert);
			}
			
			set
			{
				base.SetSystemDateTime(LabHasilMetadata.ColumnNames.TglJamInsert, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.flag_ambil
		/// </summary>
		virtual public System.String FlagAmbil
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.FlagAmbil);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.FlagAmbil, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.tgl_jam_ambil
		/// </summary>
		virtual public System.DateTime? TglJamAmbil
		{
			get
			{
				return base.GetSystemDateTime(LabHasilMetadata.ColumnNames.TglJamAmbil);
			}
			
			set
			{
				base.SetSystemDateTime(LabHasilMetadata.ColumnNames.TglJamAmbil, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.Type
		/// </summary>
		virtual public System.String Type
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.Type);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.Type, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.No_RM
		/// </summary>
		virtual public System.String NoRM
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.NoRM);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.NoRM, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.tgl_daftar
		/// </summary>
		virtual public System.DateTime? TglDaftar
		{
			get
			{
				return base.GetSystemDateTime(LabHasilMetadata.ColumnNames.TglDaftar);
			}
			
			set
			{
				base.SetSystemDateTime(LabHasilMetadata.ColumnNames.TglDaftar, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.tgl_hasil
		/// </summary>
		virtual public System.DateTime? TglHasil
		{
			get
			{
				return base.GetSystemDateTime(LabHasilMetadata.ColumnNames.TglHasil);
			}
			
			set
			{
				base.SetSystemDateTime(LabHasilMetadata.ColumnNames.TglHasil, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.kode_ruang
		/// </summary>
		virtual public System.String KodeRuang
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.KodeRuang);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.KodeRuang, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil.kode_dokter
		/// </summary>
		virtual public System.String KodeDokter
		{
			get
			{
				return base.GetSystemString(LabHasilMetadata.ColumnNames.KodeDokter);
			}
			
			set
			{
				base.SetSystemString(LabHasilMetadata.ColumnNames.KodeDokter, value);
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
			public esStrings(esLabHasil entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NoPesanan
			{
				get
				{
					System.String data = entity.NoPesanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoPesanan = null;
					else entity.NoPesanan = Convert.ToString(value);
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
				
			public System.String NoKunjungan
			{
				get
				{
					System.String data = entity.NoKunjungan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoKunjungan = null;
					else entity.NoKunjungan = Convert.ToString(value);
				}
			}
				
			public System.String KodeSir
			{
				get
				{
					System.String data = entity.KodeSir;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeSir = null;
					else entity.KodeSir = Convert.ToString(value);
				}
			}
				
			public System.String NoUrut
			{
				get
				{
					System.Int32? data = entity.NoUrut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoUrut = null;
					else entity.NoUrut = Convert.ToInt32(value);
				}
			}
				
			public System.String KodePemeriksaan
			{
				get
				{
					System.String data = entity.KodePemeriksaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodePemeriksaan = null;
					else entity.KodePemeriksaan = Convert.ToString(value);
				}
			}
				
			public System.String NamaPemeriksaan
			{
				get
				{
					System.String data = entity.NamaPemeriksaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaPemeriksaan = null;
					else entity.NamaPemeriksaan = Convert.ToString(value);
				}
			}
				
			public System.String Unit
			{
				get
				{
					System.String data = entity.Unit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Unit = null;
					else entity.Unit = Convert.ToString(value);
				}
			}
				
			public System.String Normal
			{
				get
				{
					System.String data = entity.Normal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Normal = null;
					else entity.Normal = Convert.ToString(value);
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
				
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
				
			public System.String FlagInsert
			{
				get
				{
					System.String data = entity.FlagInsert;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlagInsert = null;
					else entity.FlagInsert = Convert.ToString(value);
				}
			}
				
			public System.String TglJamInsert
			{
				get
				{
					System.DateTime? data = entity.TglJamInsert;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglJamInsert = null;
					else entity.TglJamInsert = Convert.ToDateTime(value);
				}
			}
				
			public System.String FlagAmbil
			{
				get
				{
					System.String data = entity.FlagAmbil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlagAmbil = null;
					else entity.FlagAmbil = Convert.ToString(value);
				}
			}
				
			public System.String TglJamAmbil
			{
				get
				{
					System.DateTime? data = entity.TglJamAmbil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglJamAmbil = null;
					else entity.TglJamAmbil = Convert.ToDateTime(value);
				}
			}
				
			public System.String Type
			{
				get
				{
					System.String data = entity.Type;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Type = null;
					else entity.Type = Convert.ToString(value);
				}
			}
				
			public System.String NoRM
			{
				get
				{
					System.String data = entity.NoRM;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoRM = null;
					else entity.NoRM = Convert.ToString(value);
				}
			}
				
			public System.String TglDaftar
			{
				get
				{
					System.DateTime? data = entity.TglDaftar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglDaftar = null;
					else entity.TglDaftar = Convert.ToDateTime(value);
				}
			}
				
			public System.String TglHasil
			{
				get
				{
					System.DateTime? data = entity.TglHasil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglHasil = null;
					else entity.TglHasil = Convert.ToDateTime(value);
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
				
			public System.String KodeDokter
			{
				get
				{
					System.String data = entity.KodeDokter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeDokter = null;
					else entity.KodeDokter = Convert.ToString(value);
				}
			}
			

			private esLabHasil entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabHasilQuery query)
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
				throw new Exception("esLabHasil can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LabHasil : esLabHasil
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
	abstract public class esLabHasilQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabHasilMetadata.Meta();
			}
		}	
		

		public esQueryItem NoPesanan
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoPesanan, esSystemType.String);
			}
		} 
		
		public esQueryItem NoLab
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoLab, esSystemType.String);
			}
		} 
		
		public esQueryItem NoRegistrasi
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoRegistrasi, esSystemType.String);
			}
		} 
		
		public esQueryItem NoKunjungan
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoKunjungan, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeSir
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.KodeSir, esSystemType.String);
			}
		} 
		
		public esQueryItem NoUrut
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoUrut, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KodePemeriksaan
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.KodePemeriksaan, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPemeriksaan
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NamaPemeriksaan, esSystemType.String);
			}
		} 
		
		public esQueryItem Unit
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.Unit, esSystemType.String);
			}
		} 
		
		public esQueryItem Normal
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.Normal, esSystemType.String);
			}
		} 
		
		public esQueryItem Hasil
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.Hasil, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
		public esQueryItem FlagInsert
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.FlagInsert, esSystemType.String);
			}
		} 
		
		public esQueryItem TglJamInsert
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.TglJamInsert, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem FlagAmbil
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.FlagAmbil, esSystemType.String);
			}
		} 
		
		public esQueryItem TglJamAmbil
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.TglJamAmbil, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Type
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.Type, esSystemType.String);
			}
		} 
		
		public esQueryItem NoRM
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.NoRM, esSystemType.String);
			}
		} 
		
		public esQueryItem TglDaftar
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.TglDaftar, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TglHasil
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.TglHasil, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem KodeRuang
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.KodeRuang, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeDokter
		{
			get
			{
				return new esQueryItem(this, LabHasilMetadata.ColumnNames.KodeDokter, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabHasilCollection")]
	public partial class LabHasilCollection : esLabHasilCollection, IEnumerable<LabHasil>
	{
		public LabHasilCollection()
		{

		}
		
		public static implicit operator List<LabHasil>(LabHasilCollection coll)
		{
			List<LabHasil> list = new List<LabHasil>();
			
			foreach (LabHasil emp in coll)
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
				return  LabHasilMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabHasilQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LabHasil(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LabHasil();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabHasilQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabHasilQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabHasilQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LabHasil AddNew()
		{
			LabHasil entity = base.AddNewEntity() as LabHasil;
			
			return entity;
		}

		public LabHasil FindByPrimaryKey()
		{
			return base.FindByPrimaryKey() as LabHasil;
		}


		#region IEnumerable<LabHasil> Members

		IEnumerator<LabHasil> IEnumerable<LabHasil>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LabHasil;
			}
		}

		#endregion
		
		private LabHasilQuery query;
	}


	/// <summary>
	/// Encapsulates the 'lab_hasil' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LabHasil ()")]
	[Serializable]
	public partial class LabHasil : esLabHasil
	{
		public LabHasil()
		{

		}
	
		public LabHasil(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabHasilMetadata.Meta();
			}
		}
		
		
		
		override protected esLabHasilQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabHasilQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabHasilQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabHasilQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabHasilQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabHasilQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabHasilQuery : esLabHasilQuery
	{
		public LabHasilQuery()
		{

		}		
		
		public LabHasilQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabHasilQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabHasilMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabHasilMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoPesanan, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoPesanan;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoLab, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoLab;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoRegistrasi, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoRegistrasi;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoKunjungan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoKunjungan;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.KodeSir, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.KodeSir;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoUrut, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoUrut;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.KodePemeriksaan, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.KodePemeriksaan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NamaPemeriksaan, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NamaPemeriksaan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.Unit, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.Unit;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.Normal, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.Normal;
			c.CharacterMaxLength = 350;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.Hasil, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.Hasil;
			c.CharacterMaxLength = 750;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.Flag, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.FlagInsert, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.FlagInsert;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.TglJamInsert, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabHasilMetadata.PropertyNames.TglJamInsert;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.FlagAmbil, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.FlagAmbil;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.TglJamAmbil, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabHasilMetadata.PropertyNames.TglJamAmbil;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.Type, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.Type;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.NoRM, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.NoRM;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.TglDaftar, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabHasilMetadata.PropertyNames.TglDaftar;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.TglHasil, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabHasilMetadata.PropertyNames.TglHasil;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.KodeRuang, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.KodeRuang;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilMetadata.ColumnNames.KodeDokter, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilMetadata.PropertyNames.KodeDokter;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabHasilMetadata Meta()
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
			 public const string NoPesanan = "no_pesanan";
			 public const string NoLab = "no_lab";
			 public const string NoRegistrasi = "no_registrasi";
			 public const string NoKunjungan = "no_kunjungan";
			 public const string KodeSir = "kode_sir";
			 public const string NoUrut = "no_urut";
			 public const string KodePemeriksaan = "kode_pemeriksaan";
			 public const string NamaPemeriksaan = "nama_pemeriksaan";
			 public const string Unit = "unit";
			 public const string Normal = "normal";
			 public const string Hasil = "hasil";
			 public const string Flag = "flag";
			 public const string FlagInsert = "flag_insert";
			 public const string TglJamInsert = "tgl_jam_insert";
			 public const string FlagAmbil = "flag_ambil";
			 public const string TglJamAmbil = "tgl_jam_ambil";
			 public const string Type = "Type";
			 public const string NoRM = "No_RM";
			 public const string TglDaftar = "tgl_daftar";
			 public const string TglHasil = "tgl_hasil";
			 public const string KodeRuang = "kode_ruang";
			 public const string KodeDokter = "kode_dokter";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NoPesanan = "NoPesanan";
			 public const string NoLab = "NoLab";
			 public const string NoRegistrasi = "NoRegistrasi";
			 public const string NoKunjungan = "NoKunjungan";
			 public const string KodeSir = "KodeSir";
			 public const string NoUrut = "NoUrut";
			 public const string KodePemeriksaan = "KodePemeriksaan";
			 public const string NamaPemeriksaan = "NamaPemeriksaan";
			 public const string Unit = "Unit";
			 public const string Normal = "Normal";
			 public const string Hasil = "Hasil";
			 public const string Flag = "Flag";
			 public const string FlagInsert = "FlagInsert";
			 public const string TglJamInsert = "TglJamInsert";
			 public const string FlagAmbil = "FlagAmbil";
			 public const string TglJamAmbil = "TglJamAmbil";
			 public const string Type = "Type";
			 public const string NoRM = "NoRM";
			 public const string TglDaftar = "TglDaftar";
			 public const string TglHasil = "TglHasil";
			 public const string KodeRuang = "KodeRuang";
			 public const string KodeDokter = "KodeDokter";
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
			lock (typeof(LabHasilMetadata))
			{
				if(LabHasilMetadata.mapDelegates == null)
				{
					LabHasilMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabHasilMetadata.meta == null)
				{
					LabHasilMetadata.meta = new LabHasilMetadata();
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
				

				meta.AddTypeMap("NoPesanan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoLab", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoRegistrasi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoKunjungan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeSir", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoUrut", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KodePemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Unit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Normal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Hasil", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FlagInsert", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglJamInsert", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FlagAmbil", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglJamAmbil", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Type", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoRM", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglDaftar", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TglHasil", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("KodeRuang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeDokter", new esTypeMap("varchar", "System.String"));


				meta.Source = "lab_hasil";
				meta.Destination = "lab_hasil";
				
				meta.spInsert = "proc_lab_hasilInsert";				
				meta.spUpdate = "proc_lab_hasilUpdate";		
				meta.spDelete = "proc_lab_hasilDelete";
				meta.spLoadAll = "proc_lab_hasilLoadAll";
				meta.spLoadByPrimaryKey = "proc_lab_hasilLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabHasilMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
