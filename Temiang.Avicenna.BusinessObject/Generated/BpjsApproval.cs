/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/12/2021 10:21:35 PM
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
	abstract public class esBpjsApprovalCollection : esEntityCollectionWAuditLog
	{
		public esBpjsApprovalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BpjsApprovalCollection";
		}

		#region Query Logic
		protected void InitQuery(esBpjsApprovalQuery query)
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
			this.InitQuery(query as esBpjsApprovalQuery);
		}
		#endregion
		
		virtual public BpjsApproval DetachEntity(BpjsApproval entity)
		{
			return base.DetachEntity(entity) as BpjsApproval;
		}
		
		virtual public BpjsApproval AttachEntity(BpjsApproval entity)
		{
			return base.AttachEntity(entity) as BpjsApproval;
		}
		
		virtual public void Combine(BpjsApprovalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BpjsApproval this[int index]
		{
			get
			{
				return base[index] as BpjsApproval;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BpjsApproval);
		}
	}



	[Serializable]
	abstract public class esBpjsApproval : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBpjsApprovalQuery GetDynamicQuery()
		{
			return null;
		}

		public esBpjsApproval()
		{

		}

		public esBpjsApproval(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String noKartu, System.DateTime tglSep, System.String jnsPelayanan)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(noKartu, tglSep, jnsPelayanan);
			else
				return LoadByPrimaryKeyStoredProcedure(noKartu, tglSep, jnsPelayanan);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String noKartu, System.DateTime tglSep, System.String jnsPelayanan)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(noKartu, tglSep, jnsPelayanan);
			else
				return LoadByPrimaryKeyStoredProcedure(noKartu, tglSep, jnsPelayanan);
		}

		private bool LoadByPrimaryKeyDynamic(System.String noKartu, System.DateTime tglSep, System.String jnsPelayanan)
		{
			esBpjsApprovalQuery query = this.GetDynamicQuery();
			query.Where(query.NoKartu == noKartu, query.TglSep == tglSep, query.JnsPelayanan == jnsPelayanan);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String noKartu, System.DateTime tglSep, System.String jnsPelayanan)
		{
			esParameters parms = new esParameters();
			parms.Add("noKartu", noKartu);			parms.Add("jnsPelayanan",jnsPelayanan);			parms.Add("tglSep",tglSep);
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
						case "NoKartu": this.str.NoKartu = (string)value; break;							
						case "TglSep": this.str.TglSep = (string)value; break;							
						case "JnsPelayanan": this.str.JnsPelayanan = (string)value; break;							
						case "NamaPasien": this.str.NamaPasien = (string)value; break;							
						case "JenisKelamin": this.str.JenisKelamin = (string)value; break;							
						case "Keterangan": this.str.Keterangan = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "User": this.str.User = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "JnsPengajuan": this.str.JnsPengajuan = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TglSep":
						
							if (value == null || value is System.DateTime)
								this.TglSep = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
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
		/// Maps to BpjsApproval.noKartu
		/// </summary>
		virtual public System.String NoKartu
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.NoKartu);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.NoKartu, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.tglSep
		/// </summary>
		virtual public System.DateTime? TglSep
		{
			get
			{
				return base.GetSystemDateTime(BpjsApprovalMetadata.ColumnNames.TglSep);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsApprovalMetadata.ColumnNames.TglSep, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.jnsPelayanan
		/// </summary>
		virtual public System.String JnsPelayanan
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.JnsPelayanan);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.JnsPelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.NamaPasien
		/// </summary>
		virtual public System.String NamaPasien
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.NamaPasien);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.NamaPasien, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.JenisKelamin
		/// </summary>
		virtual public System.String JenisKelamin
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.JenisKelamin);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.JenisKelamin, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.keterangan
		/// </summary>
		virtual public System.String Keterangan
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.Keterangan);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.Keterangan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(BpjsApprovalMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsApprovalMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.user
		/// </summary>
		virtual public System.String User
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.User);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.User, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BpjsApprovalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsApprovalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsApproval.jnsPengajuan
		/// </summary>
		virtual public System.String JnsPengajuan
		{
			get
			{
				return base.GetSystemString(BpjsApprovalMetadata.ColumnNames.JnsPengajuan);
			}
			
			set
			{
				base.SetSystemString(BpjsApprovalMetadata.ColumnNames.JnsPengajuan, value);
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
			public esStrings(esBpjsApproval entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NoKartu
			{
				get
				{
					System.String data = entity.NoKartu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoKartu = null;
					else entity.NoKartu = Convert.ToString(value);
				}
			}
				
			public System.String TglSep
			{
				get
				{
					System.DateTime? data = entity.TglSep;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglSep = null;
					else entity.TglSep = Convert.ToDateTime(value);
				}
			}
				
			public System.String JnsPelayanan
			{
				get
				{
					System.String data = entity.JnsPelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JnsPelayanan = null;
					else entity.JnsPelayanan = Convert.ToString(value);
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
				
			public System.String Keterangan
			{
				get
				{
					System.String data = entity.Keterangan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Keterangan = null;
					else entity.Keterangan = Convert.ToString(value);
				}
			}
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
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
				
			public System.String JnsPengajuan
			{
				get
				{
					System.String data = entity.JnsPengajuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JnsPengajuan = null;
					else entity.JnsPengajuan = Convert.ToString(value);
				}
			}
			

			private esBpjsApproval entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBpjsApprovalQuery query)
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
				throw new Exception("esBpjsApproval can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esBpjsApprovalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BpjsApprovalMetadata.Meta();
			}
		}	
		

		public esQueryItem NoKartu
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.NoKartu, esSystemType.String);
			}
		} 
		
		public esQueryItem TglSep
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.TglSep, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem JnsPelayanan
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.JnsPelayanan, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPasien
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.NamaPasien, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisKelamin
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.JenisKelamin, esSystemType.String);
			}
		} 
		
		public esQueryItem Keterangan
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.Keterangan, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem User
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.User, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem JnsPengajuan
		{
			get
			{
				return new esQueryItem(this, BpjsApprovalMetadata.ColumnNames.JnsPengajuan, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BpjsApprovalCollection")]
	public partial class BpjsApprovalCollection : esBpjsApprovalCollection, IEnumerable<BpjsApproval>
	{
		public BpjsApprovalCollection()
		{

		}
		
		public static implicit operator List<BpjsApproval>(BpjsApprovalCollection coll)
		{
			List<BpjsApproval> list = new List<BpjsApproval>();
			
			foreach (BpjsApproval emp in coll)
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
				return  BpjsApprovalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsApprovalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BpjsApproval(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BpjsApproval();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BpjsApprovalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsApprovalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BpjsApprovalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BpjsApproval AddNew()
		{
			BpjsApproval entity = base.AddNewEntity() as BpjsApproval;
			
			return entity;
		}

		public BpjsApproval FindByPrimaryKey(System.String jnsPelayanan, System.String noKartu, System.DateTime tglSep)
		{
			return base.FindByPrimaryKey(jnsPelayanan, noKartu, tglSep) as BpjsApproval;
		}


		#region IEnumerable<BpjsApproval> Members

		IEnumerator<BpjsApproval> IEnumerable<BpjsApproval>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BpjsApproval;
			}
		}

		#endregion
		
		private BpjsApprovalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BpjsApproval' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BpjsApproval ({NoKartu},{TglSep},{JnsPelayanan})")]
	[Serializable]
	public partial class BpjsApproval : esBpjsApproval
	{
		public BpjsApproval()
		{

		}
	
		public BpjsApproval(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BpjsApprovalMetadata.Meta();
			}
		}
		
		
		
		override protected esBpjsApprovalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsApprovalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BpjsApprovalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsApprovalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BpjsApprovalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BpjsApprovalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BpjsApprovalQuery : esBpjsApprovalQuery
	{
		public BpjsApprovalQuery()
		{

		}		
		
		public BpjsApprovalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BpjsApprovalQuery";
        }
		
			
	}


	[Serializable]
	public partial class BpjsApprovalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BpjsApprovalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.NoKartu, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.NoKartu;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.TglSep, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.TglSep;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.JnsPelayanan, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.JnsPelayanan;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.NamaPasien, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.NamaPasien;
			c.CharacterMaxLength = 2550;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.JenisKelamin, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.JenisKelamin;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.Keterangan, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.Keterangan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.User, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.User;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsApprovalMetadata.ColumnNames.JnsPengajuan, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsApprovalMetadata.PropertyNames.JnsPengajuan;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BpjsApprovalMetadata Meta()
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
			 public const string NoKartu = "noKartu";
			 public const string TglSep = "tglSep";
			 public const string JnsPelayanan = "jnsPelayanan";
			 public const string NamaPasien = "NamaPasien";
			 public const string JenisKelamin = "JenisKelamin";
			 public const string Keterangan = "keterangan";
			 public const string IsApproved = "IsApproved";
			 public const string User = "user";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string JnsPengajuan = "jnsPengajuan";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NoKartu = "NoKartu";
			 public const string TglSep = "TglSep";
			 public const string JnsPelayanan = "JnsPelayanan";
			 public const string NamaPasien = "NamaPasien";
			 public const string JenisKelamin = "JenisKelamin";
			 public const string Keterangan = "Keterangan";
			 public const string IsApproved = "IsApproved";
			 public const string User = "User";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string JnsPengajuan = "JnsPengajuan";
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
			lock (typeof(BpjsApprovalMetadata))
			{
				if(BpjsApprovalMetadata.mapDelegates == null)
				{
					BpjsApprovalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BpjsApprovalMetadata.meta == null)
				{
					BpjsApprovalMetadata.meta = new BpjsApprovalMetadata();
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
				

				meta.AddTypeMap("NoKartu", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglSep", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("JnsPelayanan", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("NamaPasien", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisKelamin", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Keterangan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("User", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JnsPengajuan", new esTypeMap("char", "System.String"));			
				
				
				
				meta.Source = "BpjsApproval";
				meta.Destination = "BpjsApproval";
				
				meta.spInsert = "proc_BpjsApprovalInsert";				
				meta.spUpdate = "proc_BpjsApprovalUpdate";		
				meta.spDelete = "proc_BpjsApprovalDelete";
				meta.spLoadAll = "proc_BpjsApprovalLoadAll";
				meta.spLoadByPrimaryKey = "proc_BpjsApprovalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BpjsApprovalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
