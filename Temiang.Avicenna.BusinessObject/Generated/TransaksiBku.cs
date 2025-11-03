/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/1/2021 4:31:16 PM
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
	abstract public class esTransaksiBkuCollection : esEntityCollectionWAuditLog
	{
		public esTransaksiBkuCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransaksiBkuCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransaksiBkuQuery query)
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
			this.InitQuery(query as esTransaksiBkuQuery);
		}
		#endregion
		
		virtual public TransaksiBku DetachEntity(TransaksiBku entity)
		{
			return base.DetachEntity(entity) as TransaksiBku;
		}
		
		virtual public TransaksiBku AttachEntity(TransaksiBku entity)
		{
			return base.AttachEntity(entity) as TransaksiBku;
		}
		
		virtual public void Combine(TransaksiBkuCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransaksiBku this[int index]
		{
			get
			{
				return base[index] as TransaksiBku;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransaksiBku);
		}
	}



	[Serializable]
	abstract public class esTransaksiBku : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransaksiBkuQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransaksiBku()
		{

		}

		public esTransaksiBku(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String nomor)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nomor);
			else
				return LoadByPrimaryKeyStoredProcedure(nomor);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String nomor)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nomor);
			else
				return LoadByPrimaryKeyStoredProcedure(nomor);
		}

		private bool LoadByPrimaryKeyDynamic(System.String nomor)
		{
			esTransaksiBkuQuery query = this.GetDynamicQuery();
			query.Where(query.Nomor == nomor);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String nomor)
		{
			esParameters parms = new esParameters();
			parms.Add("Nomor",nomor);
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
						case "Nomor": this.str.Nomor = (string)value; break;							
						case "Jenis": this.str.Jenis = (string)value; break;							
						case "Pelanggan": this.str.Pelanggan = (string)value; break;							
						case "Unit": this.str.Unit = (string)value; break;							
						case "Tanggal": this.str.Tanggal = (string)value; break;							
						case "Uraian": this.str.Uraian = (string)value; break;							
						case "KasBank": this.str.KasBank = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Jenis":
						
							if (value == null || value is System.Byte)
								this.Jenis = (System.Byte?)value;
							break;
						
						case "Tanggal":
						
							if (value == null || value is System.DateTime)
								this.Tanggal = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to TransaksiBku.Nomor
		/// </summary>
		virtual public System.String Nomor
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.Nomor);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.Nomor, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.Jenis
		/// </summary>
		virtual public System.Byte? Jenis
		{
			get
			{
				return base.GetSystemByte(TransaksiBkuMetadata.ColumnNames.Jenis);
			}
			
			set
			{
				base.SetSystemByte(TransaksiBkuMetadata.ColumnNames.Jenis, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.Pelanggan
		/// </summary>
		virtual public System.String Pelanggan
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.Pelanggan);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.Pelanggan, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.Unit
		/// </summary>
		virtual public System.String Unit
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.Unit);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.Unit, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.Tanggal
		/// </summary>
		virtual public System.DateTime? Tanggal
		{
			get
			{
				return base.GetSystemDateTime(TransaksiBkuMetadata.ColumnNames.Tanggal);
			}
			
			set
			{
				base.SetSystemDateTime(TransaksiBkuMetadata.ColumnNames.Tanggal, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.Uraian
		/// </summary>
		virtual public System.String Uraian
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.Uraian);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.Uraian, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.KasBank
		/// </summary>
		virtual public System.String KasBank
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.KasBank);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.KasBank, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransaksiBkuMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(TransaksiBkuMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransaksiBkuMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransaksiBkuMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransaksiBkuMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransaksiBkuMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransaksiBku.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransaksiBkuMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransaksiBkuMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransaksiBku entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Nomor
			{
				get
				{
					System.String data = entity.Nomor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nomor = null;
					else entity.Nomor = Convert.ToString(value);
				}
			}
				
			public System.String Jenis
			{
				get
				{
					System.Byte? data = entity.Jenis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jenis = null;
					else entity.Jenis = Convert.ToByte(value);
				}
			}
				
			public System.String Pelanggan
			{
				get
				{
					System.String data = entity.Pelanggan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pelanggan = null;
					else entity.Pelanggan = Convert.ToString(value);
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
				
			public System.String Tanggal
			{
				get
				{
					System.DateTime? data = entity.Tanggal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggal = null;
					else entity.Tanggal = Convert.ToDateTime(value);
				}
			}
				
			public System.String Uraian
			{
				get
				{
					System.String data = entity.Uraian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Uraian = null;
					else entity.Uraian = Convert.ToString(value);
				}
			}
				
			public System.String KasBank
			{
				get
				{
					System.String data = entity.KasBank;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KasBank = null;
					else entity.KasBank = Convert.ToString(value);
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
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			

			private esTransaksiBku entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransaksiBkuQuery query)
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
				throw new Exception("esTransaksiBku can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTransaksiBkuQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransaksiBkuMetadata.Meta();
			}
		}	
		

		public esQueryItem Nomor
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Nomor, esSystemType.String);
			}
		} 
		
		public esQueryItem Jenis
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Jenis, esSystemType.Byte);
			}
		} 
		
		public esQueryItem Pelanggan
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Pelanggan, esSystemType.String);
			}
		} 
		
		public esQueryItem Unit
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Unit, esSystemType.String);
			}
		} 
		
		public esQueryItem Tanggal
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Tanggal, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Uraian
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.Uraian, esSystemType.String);
			}
		} 
		
		public esQueryItem KasBank
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.KasBank, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransaksiBkuMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransaksiBkuCollection")]
	public partial class TransaksiBkuCollection : esTransaksiBkuCollection, IEnumerable<TransaksiBku>
	{
		public TransaksiBkuCollection()
		{

		}
		
		public static implicit operator List<TransaksiBku>(TransaksiBkuCollection coll)
		{
			List<TransaksiBku> list = new List<TransaksiBku>();
			
			foreach (TransaksiBku emp in coll)
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
				return  TransaksiBkuMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransaksiBkuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransaksiBku(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransaksiBku();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransaksiBkuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransaksiBkuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransaksiBkuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransaksiBku AddNew()
		{
			TransaksiBku entity = base.AddNewEntity() as TransaksiBku;
			
			return entity;
		}

		public TransaksiBku FindByPrimaryKey(System.String nomor)
		{
			return base.FindByPrimaryKey(nomor) as TransaksiBku;
		}


		#region IEnumerable<TransaksiBku> Members

		IEnumerator<TransaksiBku> IEnumerable<TransaksiBku>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransaksiBku;
			}
		}

		#endregion
		
		private TransaksiBkuQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransaksiBku' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransaksiBku ({Nomor})")]
	[Serializable]
	public partial class TransaksiBku : esTransaksiBku
	{
		public TransaksiBku()
		{

		}
	
		public TransaksiBku(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransaksiBkuMetadata.Meta();
			}
		}
		
		
		
		override protected esTransaksiBkuQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransaksiBkuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransaksiBkuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransaksiBkuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransaksiBkuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransaksiBkuQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransaksiBkuQuery : esTransaksiBkuQuery
	{
		public TransaksiBkuQuery()
		{

		}		
		
		public TransaksiBkuQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransaksiBkuQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransaksiBkuMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransaksiBkuMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Nomor, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Nomor;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Jenis, 1, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Jenis;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Pelanggan, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Pelanggan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Unit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Unit;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Tanggal, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Tanggal;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.Uraian, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.Uraian;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.KasBank, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.KasBank;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransaksiBkuMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransaksiBkuMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransaksiBkuMetadata Meta()
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
			 public const string Nomor = "Nomor";
			 public const string Jenis = "Jenis";
			 public const string Pelanggan = "Pelanggan";
			 public const string Unit = "Unit";
			 public const string Tanggal = "Tanggal";
			 public const string Uraian = "Uraian";
			 public const string KasBank = "KasBank";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Nomor = "Nomor";
			 public const string Jenis = "Jenis";
			 public const string Pelanggan = "Pelanggan";
			 public const string Unit = "Unit";
			 public const string Tanggal = "Tanggal";
			 public const string Uraian = "Uraian";
			 public const string KasBank = "KasBank";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(TransaksiBkuMetadata))
			{
				if(TransaksiBkuMetadata.mapDelegates == null)
				{
					TransaksiBkuMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransaksiBkuMetadata.meta == null)
				{
					TransaksiBkuMetadata.meta = new TransaksiBkuMetadata();
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
				

				meta.AddTypeMap("Nomor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jenis", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("Pelanggan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Unit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Tanggal", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Uraian", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KasBank", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransaksiBku";
				meta.Destination = "TransaksiBku";
				
				meta.spInsert = "proc_TransaksiBkuInsert";				
				meta.spUpdate = "proc_TransaksiBkuUpdate";		
				meta.spDelete = "proc_TransaksiBkuDelete";
				meta.spLoadAll = "proc_TransaksiBkuLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransaksiBkuLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransaksiBkuMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
