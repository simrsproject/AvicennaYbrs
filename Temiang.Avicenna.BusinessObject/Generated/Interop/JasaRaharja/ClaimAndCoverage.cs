/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/18/2016 2:09:59 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.JasaRaharja
{

	[Serializable]
	abstract public class esClaimAndCoverageCollection : esEntityCollectionWAuditLog
	{
		public esClaimAndCoverageCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ClaimAndCoverageCollection";
		}

		#region Query Logic
		protected void InitQuery(esClaimAndCoverageQuery query)
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
			this.InitQuery(query as esClaimAndCoverageQuery);
		}
		#endregion
		
		virtual public ClaimAndCoverage DetachEntity(ClaimAndCoverage entity)
		{
			return base.DetachEntity(entity) as ClaimAndCoverage;
		}
		
		virtual public ClaimAndCoverage AttachEntity(ClaimAndCoverage entity)
		{
			return base.AttachEntity(entity) as ClaimAndCoverage;
		}
		
		virtual public void Combine(ClaimAndCoverageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ClaimAndCoverage this[int index]
		{
			get
			{
				return base[index] as ClaimAndCoverage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClaimAndCoverage);
		}
	}



	[Serializable]
	abstract public class esClaimAndCoverage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClaimAndCoverageQuery GetDynamicQuery()
		{
			return null;
		}

		public esClaimAndCoverage()
		{

		}

		public esClaimAndCoverage(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esClaimAndCoverageQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "IdRegister": this.str.IdRegister = (string)value; break;							
						case "SifatCedera": this.str.SifatCedera = (string)value; break;							
						case "JenisTindakan": this.str.JenisTindakan = (string)value; break;							
						case "DokterBerwenang": this.str.DokterBerwenang = (string)value; break;							
						case "JmlBiaya": this.str.JmlBiaya = (string)value; break;							
						case "JmlKlaim": this.str.JmlKlaim = (string)value; break;							
						case "TglProses": this.str.TglProses = (string)value; break;							
						case "StatusJaminan": this.str.StatusJaminan = (string)value; break;							
						case "StatusKlaim": this.str.StatusKlaim = (string)value; break;							
						case "NoSuratJaminan": this.str.NoSuratJaminan = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JmlBiaya":
						
							if (value == null || value is System.Decimal)
								this.JmlBiaya = (System.Decimal?)value;
							break;
						
						case "JmlKlaim":
						
							if (value == null || value is System.Decimal)
								this.JmlKlaim = (System.Decimal?)value;
							break;
						
						case "TglProses":
						
							if (value == null || value is System.DateTime)
								this.TglProses = (System.DateTime?)value;
							break;
						
						case "StatusKlaim":
						
							if (value == null || value is System.Boolean)
								this.StatusKlaim = (System.Boolean?)value;
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
		/// Maps to ClaimAndCoverage.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.IdRegister
		/// </summary>
		virtual public System.String IdRegister
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.IdRegister);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.IdRegister, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.SifatCedera
		/// </summary>
		virtual public System.String SifatCedera
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.SifatCedera);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.SifatCedera, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.JenisTindakan
		/// </summary>
		virtual public System.String JenisTindakan
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.JenisTindakan);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.JenisTindakan, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.DokterBerwenang
		/// </summary>
		virtual public System.String DokterBerwenang
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.DokterBerwenang);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.DokterBerwenang, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.JmlBiaya
		/// </summary>
		virtual public System.Decimal? JmlBiaya
		{
			get
			{
				return base.GetSystemDecimal(ClaimAndCoverageMetadata.ColumnNames.JmlBiaya);
			}
			
			set
			{
				base.SetSystemDecimal(ClaimAndCoverageMetadata.ColumnNames.JmlBiaya, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.JmlKlaim
		/// </summary>
		virtual public System.Decimal? JmlKlaim
		{
			get
			{
				return base.GetSystemDecimal(ClaimAndCoverageMetadata.ColumnNames.JmlKlaim);
			}
			
			set
			{
				base.SetSystemDecimal(ClaimAndCoverageMetadata.ColumnNames.JmlKlaim, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.TglProses
		/// </summary>
		virtual public System.DateTime? TglProses
		{
			get
			{
				return base.GetSystemDateTime(ClaimAndCoverageMetadata.ColumnNames.TglProses);
			}
			
			set
			{
				base.SetSystemDateTime(ClaimAndCoverageMetadata.ColumnNames.TglProses, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.StatusJaminan
		/// </summary>
		virtual public System.String StatusJaminan
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.StatusJaminan);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.StatusJaminan, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.StatusKlaim
		/// </summary>
		virtual public System.Boolean? StatusKlaim
		{
			get
			{
				return base.GetSystemBoolean(ClaimAndCoverageMetadata.ColumnNames.StatusKlaim);
			}
			
			set
			{
				base.SetSystemBoolean(ClaimAndCoverageMetadata.ColumnNames.StatusKlaim, value);
			}
		}
		
		/// <summary>
		/// Maps to ClaimAndCoverage.NoSuratJaminan
		/// </summary>
		virtual public System.String NoSuratJaminan
		{
			get
			{
				return base.GetSystemString(ClaimAndCoverageMetadata.ColumnNames.NoSuratJaminan);
			}
			
			set
			{
				base.SetSystemString(ClaimAndCoverageMetadata.ColumnNames.NoSuratJaminan, value);
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
			public esStrings(esClaimAndCoverage entity)
			{
				this.entity = entity;
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
				
			public System.String IdRegister
			{
				get
				{
					System.String data = entity.IdRegister;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdRegister = null;
					else entity.IdRegister = Convert.ToString(value);
				}
			}
				
			public System.String SifatCedera
			{
				get
				{
					System.String data = entity.SifatCedera;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SifatCedera = null;
					else entity.SifatCedera = Convert.ToString(value);
				}
			}
				
			public System.String JenisTindakan
			{
				get
				{
					System.String data = entity.JenisTindakan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisTindakan = null;
					else entity.JenisTindakan = Convert.ToString(value);
				}
			}
				
			public System.String DokterBerwenang
			{
				get
				{
					System.String data = entity.DokterBerwenang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DokterBerwenang = null;
					else entity.DokterBerwenang = Convert.ToString(value);
				}
			}
				
			public System.String JmlBiaya
			{
				get
				{
					System.Decimal? data = entity.JmlBiaya;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JmlBiaya = null;
					else entity.JmlBiaya = Convert.ToDecimal(value);
				}
			}
				
			public System.String JmlKlaim
			{
				get
				{
					System.Decimal? data = entity.JmlKlaim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JmlKlaim = null;
					else entity.JmlKlaim = Convert.ToDecimal(value);
				}
			}
				
			public System.String TglProses
			{
				get
				{
					System.DateTime? data = entity.TglProses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglProses = null;
					else entity.TglProses = Convert.ToDateTime(value);
				}
			}
				
			public System.String StatusJaminan
			{
				get
				{
					System.String data = entity.StatusJaminan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusJaminan = null;
					else entity.StatusJaminan = Convert.ToString(value);
				}
			}
				
			public System.String StatusKlaim
			{
				get
				{
					System.Boolean? data = entity.StatusKlaim;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusKlaim = null;
					else entity.StatusKlaim = Convert.ToBoolean(value);
				}
			}
				
			public System.String NoSuratJaminan
			{
				get
				{
					System.String data = entity.NoSuratJaminan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoSuratJaminan = null;
					else entity.NoSuratJaminan = Convert.ToString(value);
				}
			}
			

			private esClaimAndCoverage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClaimAndCoverageQuery query)
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
				throw new Exception("esClaimAndCoverage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esClaimAndCoverageQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ClaimAndCoverageMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IdRegister
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.IdRegister, esSystemType.String);
			}
		} 
		
		public esQueryItem SifatCedera
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.SifatCedera, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisTindakan
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.JenisTindakan, esSystemType.String);
			}
		} 
		
		public esQueryItem DokterBerwenang
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.DokterBerwenang, esSystemType.String);
			}
		} 
		
		public esQueryItem JmlBiaya
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.JmlBiaya, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem JmlKlaim
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.JmlKlaim, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TglProses
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.TglProses, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem StatusJaminan
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.StatusJaminan, esSystemType.String);
			}
		} 
		
		public esQueryItem StatusKlaim
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.StatusKlaim, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem NoSuratJaminan
		{
			get
			{
				return new esQueryItem(this, ClaimAndCoverageMetadata.ColumnNames.NoSuratJaminan, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClaimAndCoverageCollection")]
	public partial class ClaimAndCoverageCollection : esClaimAndCoverageCollection, IEnumerable<ClaimAndCoverage>
	{
		public ClaimAndCoverageCollection()
		{

		}
		
		public static implicit operator List<ClaimAndCoverage>(ClaimAndCoverageCollection coll)
		{
			List<ClaimAndCoverage> list = new List<ClaimAndCoverage>();
			
			foreach (ClaimAndCoverage emp in coll)
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
				return  ClaimAndCoverageMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClaimAndCoverageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClaimAndCoverage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClaimAndCoverage();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ClaimAndCoverageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClaimAndCoverageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ClaimAndCoverageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ClaimAndCoverage AddNew()
		{
			ClaimAndCoverage entity = base.AddNewEntity() as ClaimAndCoverage;
			
			return entity;
		}

		public ClaimAndCoverage FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as ClaimAndCoverage;
		}


		#region IEnumerable<ClaimAndCoverage> Members

		IEnumerator<ClaimAndCoverage> IEnumerable<ClaimAndCoverage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ClaimAndCoverage;
			}
		}

		#endregion
		
		private ClaimAndCoverageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClaimAndCoverage' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ClaimAndCoverage ({RegistrationNo})")]
	[Serializable]
	public partial class ClaimAndCoverage : esClaimAndCoverage
	{
		public ClaimAndCoverage()
		{

		}
	
		public ClaimAndCoverage(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClaimAndCoverageMetadata.Meta();
			}
		}
		
		
		
		override protected esClaimAndCoverageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClaimAndCoverageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ClaimAndCoverageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClaimAndCoverageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ClaimAndCoverageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ClaimAndCoverageQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ClaimAndCoverageQuery : esClaimAndCoverageQuery
	{
		public ClaimAndCoverageQuery()
		{

		}		
		
		public ClaimAndCoverageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ClaimAndCoverageQuery";
        }
		
			
	}


	[Serializable]
	public partial class ClaimAndCoverageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClaimAndCoverageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.IdRegister, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.IdRegister;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.SifatCedera, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.SifatCedera;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.JenisTindakan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.JenisTindakan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.DokterBerwenang, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.DokterBerwenang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.JmlBiaya, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.JmlBiaya;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.JmlKlaim, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.JmlKlaim;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.TglProses, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.TglProses;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.StatusJaminan, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.StatusJaminan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.StatusKlaim, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.StatusKlaim;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClaimAndCoverageMetadata.ColumnNames.NoSuratJaminan, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ClaimAndCoverageMetadata.PropertyNames.NoSuratJaminan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ClaimAndCoverageMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IdRegister = "IdRegister";
			 public const string SifatCedera = "SifatCedera";
			 public const string JenisTindakan = "JenisTindakan";
			 public const string DokterBerwenang = "DokterBerwenang";
			 public const string JmlBiaya = "JmlBiaya";
			 public const string JmlKlaim = "JmlKlaim";
			 public const string TglProses = "TglProses";
			 public const string StatusJaminan = "StatusJaminan";
			 public const string StatusKlaim = "StatusKlaim";
			 public const string NoSuratJaminan = "NoSuratJaminan";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IdRegister = "IdRegister";
			 public const string SifatCedera = "SifatCedera";
			 public const string JenisTindakan = "JenisTindakan";
			 public const string DokterBerwenang = "DokterBerwenang";
			 public const string JmlBiaya = "JmlBiaya";
			 public const string JmlKlaim = "JmlKlaim";
			 public const string TglProses = "TglProses";
			 public const string StatusJaminan = "StatusJaminan";
			 public const string StatusKlaim = "StatusKlaim";
			 public const string NoSuratJaminan = "NoSuratJaminan";
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
			lock (typeof(ClaimAndCoverageMetadata))
			{
				if(ClaimAndCoverageMetadata.mapDelegates == null)
				{
					ClaimAndCoverageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ClaimAndCoverageMetadata.meta == null)
				{
					ClaimAndCoverageMetadata.meta = new ClaimAndCoverageMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IdRegister", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SifatCedera", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisTindakan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DokterBerwenang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JmlBiaya", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("JmlKlaim", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TglProses", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("StatusJaminan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StatusKlaim", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NoSuratJaminan", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ClaimAndCoverage";
				meta.Destination = "ClaimAndCoverage";
				
				meta.spInsert = "proc_ClaimAndCoverageInsert";				
				meta.spUpdate = "proc_ClaimAndCoverageUpdate";		
				meta.spDelete = "proc_ClaimAndCoverageDelete";
				meta.spLoadAll = "proc_ClaimAndCoverageLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClaimAndCoverageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClaimAndCoverageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
