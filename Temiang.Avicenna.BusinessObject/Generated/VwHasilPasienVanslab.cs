/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/28/2018 6:54:10 PM
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
	abstract public class esVwHasilPasienVanslabCollection : esEntityCollectionWAuditLog
	{
		public esVwHasilPasienVanslabCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwHasilPasienVanslabCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwHasilPasienVanslabQuery query)
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
			this.InitQuery(query as esVwHasilPasienVanslabQuery);
		}
		#endregion
		
		virtual public VwHasilPasienVanslab DetachEntity(VwHasilPasienVanslab entity)
		{
			return base.DetachEntity(entity) as VwHasilPasienVanslab;
		}
		
		virtual public VwHasilPasienVanslab AttachEntity(VwHasilPasienVanslab entity)
		{
			return base.AttachEntity(entity) as VwHasilPasienVanslab;
		}
		
		virtual public void Combine(VwHasilPasienVanslabCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwHasilPasienVanslab this[int index]
		{
			get
			{
				return base[index] as VwHasilPasienVanslab;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwHasilPasienVanslab);
		}
	}



	[Serializable]
	abstract public class esVwHasilPasienVanslab : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwHasilPasienVanslabQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwHasilPasienVanslab()
		{

		}

		public esVwHasilPasienVanslab(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "NoUrut": this.str.NoUrut = (string)value; break;							
						case "KodePemeriksaan": this.str.KodePemeriksaan = (string)value; break;							
						case "NamaPemeriksaan": this.str.NamaPemeriksaan = (string)value; break;							
						case "Hasil": this.str.Hasil = (string)value; break;							
						case "StandardValue": this.str.StandardValue = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "KodeTest": this.str.KodeTest = (string)value; break;							
						case "Teks": this.str.Teks = (string)value; break;							
						case "Unit": this.str.Unit = (string)value; break;							
						case "Normal": this.str.Normal = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;
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
		/// Maps to vw_HasilPasienVanslab.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.no_urut
		/// </summary>
		virtual public System.Int32? NoUrut
		{
			get
			{
				return base.GetSystemInt32(VwHasilPasienVanslabMetadata.ColumnNames.NoUrut);
			}
			
			set
			{
				base.SetSystemInt32(VwHasilPasienVanslabMetadata.ColumnNames.NoUrut, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.kode_pemeriksaan
		/// </summary>
		virtual public System.String KodePemeriksaan
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.KodePemeriksaan);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.KodePemeriksaan, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.nama_pemeriksaan
		/// </summary>
		virtual public System.String NamaPemeriksaan
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.NamaPemeriksaan);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.NamaPemeriksaan, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.hasil
		/// </summary>
		virtual public System.String Hasil
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Hasil);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Hasil, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.StandardValue
		/// </summary>
		virtual public System.String StandardValue
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.StandardValue);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.StandardValue, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.kode_test
		/// </summary>
		virtual public System.String KodeTest
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.KodeTest);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.KodeTest, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.teks
		/// </summary>
		virtual public System.String Teks
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Teks);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Teks, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.unit
		/// </summary>
		virtual public System.String Unit
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Unit);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Unit, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.normal
		/// </summary>
		virtual public System.String Normal
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Normal);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Normal, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasienVanslab.flag
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienVanslabMetadata.ColumnNames.Flag, value);
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
			public esStrings(esVwHasilPasienVanslab entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
				
			public System.String StandardValue
			{
				get
				{
					System.String data = entity.StandardValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardValue = null;
					else entity.StandardValue = Convert.ToString(value);
				}
			}
				
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
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
				
			public System.String Teks
			{
				get
				{
					System.String data = entity.Teks;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Teks = null;
					else entity.Teks = Convert.ToString(value);
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
			

			private esVwHasilPasienVanslab entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwHasilPasienVanslabQuery query)
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
				throw new Exception("esVwHasilPasienVanslab can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwHasilPasienVanslabQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilPasienVanslabMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem NoUrut
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.NoUrut, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KodePemeriksaan
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.KodePemeriksaan, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPemeriksaan
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.NamaPemeriksaan, esSystemType.String);
			}
		} 
		
		public esQueryItem Hasil
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.Hasil, esSystemType.String);
			}
		} 
		
		public esQueryItem StandardValue
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.StandardValue, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTest
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.KodeTest, esSystemType.String);
			}
		} 
		
		public esQueryItem Teks
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.Teks, esSystemType.String);
			}
		} 
		
		public esQueryItem Unit
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.Unit, esSystemType.String);
			}
		} 
		
		public esQueryItem Normal
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.Normal, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienVanslabMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwHasilPasienVanslabCollection")]
	public partial class VwHasilPasienVanslabCollection : esVwHasilPasienVanslabCollection, IEnumerable<VwHasilPasienVanslab>
	{
		public VwHasilPasienVanslabCollection()
		{

		}
		
		public static implicit operator List<VwHasilPasienVanslab>(VwHasilPasienVanslabCollection coll)
		{
			List<VwHasilPasienVanslab> list = new List<VwHasilPasienVanslab>();
			
			foreach (VwHasilPasienVanslab emp in coll)
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
				return  VwHasilPasienVanslabMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilPasienVanslabQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwHasilPasienVanslab(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwHasilPasienVanslab();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwHasilPasienVanslabQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilPasienVanslabQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwHasilPasienVanslabQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwHasilPasienVanslab AddNew()
		{
			VwHasilPasienVanslab entity = base.AddNewEntity() as VwHasilPasienVanslab;
			
			return entity;
		}


		#region IEnumerable<VwHasilPasienVanslab> Members

		IEnumerator<VwHasilPasienVanslab> IEnumerable<VwHasilPasienVanslab>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwHasilPasienVanslab;
			}
		}

		#endregion
		
		private VwHasilPasienVanslabQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_HasilPasienVanslab' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwHasilPasienVanslab ()")]
	[Serializable]
	public partial class VwHasilPasienVanslab : esVwHasilPasienVanslab
	{
		public VwHasilPasienVanslab()
		{

		}
	
		public VwHasilPasienVanslab(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilPasienVanslabMetadata.Meta();
			}
		}
		
		
		
		override protected esVwHasilPasienVanslabQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilPasienVanslabQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwHasilPasienVanslabQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilPasienVanslabQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwHasilPasienVanslabQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwHasilPasienVanslabQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwHasilPasienVanslabQuery : esVwHasilPasienVanslabQuery
	{
		public VwHasilPasienVanslabQuery()
		{

		}		
		
		public VwHasilPasienVanslabQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwHasilPasienVanslabQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwHasilPasienVanslabMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwHasilPasienVanslabMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.NoUrut, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.NoUrut;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.KodePemeriksaan, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.KodePemeriksaan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.NamaPemeriksaan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.NamaPemeriksaan;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.Hasil, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.Hasil;
			c.CharacterMaxLength = 3500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.StandardValue, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.StandardValue;
			c.CharacterMaxLength = 401;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.RegistrationNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.KodeTest, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.KodeTest;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.Teks, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.Teks;
			c.CharacterMaxLength = 3500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.Unit, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.Unit;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.Normal, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.Normal;
			c.CharacterMaxLength = 350;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienVanslabMetadata.ColumnNames.Flag, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienVanslabMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwHasilPasienVanslabMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string NoUrut = "no_urut";
			 public const string KodePemeriksaan = "kode_pemeriksaan";
			 public const string NamaPemeriksaan = "nama_pemeriksaan";
			 public const string Hasil = "hasil";
			 public const string StandardValue = "StandardValue";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string KodeTest = "kode_test";
			 public const string Teks = "teks";
			 public const string Unit = "unit";
			 public const string Normal = "normal";
			 public const string Flag = "flag";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string NoUrut = "NoUrut";
			 public const string KodePemeriksaan = "KodePemeriksaan";
			 public const string NamaPemeriksaan = "NamaPemeriksaan";
			 public const string Hasil = "Hasil";
			 public const string StandardValue = "StandardValue";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string KodeTest = "KodeTest";
			 public const string Teks = "Teks";
			 public const string Unit = "Unit";
			 public const string Normal = "Normal";
			 public const string Flag = "Flag";
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
			lock (typeof(VwHasilPasienVanslabMetadata))
			{
				if(VwHasilPasienVanslabMetadata.mapDelegates == null)
				{
					VwHasilPasienVanslabMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwHasilPasienVanslabMetadata.meta == null)
				{
					VwHasilPasienVanslabMetadata.meta = new VwHasilPasienVanslabMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoUrut", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KodePemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Hasil", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StandardValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeTest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Teks", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Unit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Normal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_HasilPasienVanslab";
				meta.Destination = "vw_HasilPasienVanslab";
				
				meta.spInsert = "proc_vw_HasilPasienVanslabInsert";				
				meta.spUpdate = "proc_vw_HasilPasienVanslabUpdate";		
				meta.spDelete = "proc_vw_HasilPasienVanslabDelete";
				meta.spLoadAll = "proc_vw_HasilPasienVanslabLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_HasilPasienVanslabLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwHasilPasienVanslabMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
