/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPkpCollection : esEntityCollectionWAuditLog
	{
		public esPkpCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PkpCollection";
		}

		#region Query Logic
		protected void InitQuery(esPkpQuery query)
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
			this.InitQuery(query as esPkpQuery);
		}
		#endregion
		
		virtual public Pkp DetachEntity(Pkp entity)
		{
			return base.DetachEntity(entity) as Pkp;
		}
		
		virtual public Pkp AttachEntity(Pkp entity)
		{
			return base.AttachEntity(entity) as Pkp;
		}
		
		virtual public void Combine(PkpCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Pkp this[int index]
		{
			get
			{
				return base[index] as Pkp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Pkp);
		}
	}



	[Serializable]
	abstract public class esPkp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPkpQuery GetDynamicQuery()
		{
			return null;
		}

		public esPkp()
		{

		}

		public esPkp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 pkpID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pkpID);
			else
				return LoadByPrimaryKeyStoredProcedure(pkpID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 pkpID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pkpID);
			else
				return LoadByPrimaryKeyStoredProcedure(pkpID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 pkpID)
		{
			esPkpQuery query = this.GetDynamicQuery();
			query.Where(query.PkpID == pkpID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 pkpID)
		{
			esParameters parms = new esParameters();
			parms.Add("PkpID",pkpID);
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
						case "PkpID": this.str.PkpID = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "IsNPWP": this.str.IsNPWP = (string)value; break;							
						case "LowerLimit": this.str.LowerLimit = (string)value; break;							
						case "UpperLimit": this.str.UpperLimit = (string)value; break;							
						case "TaxRate": this.str.TaxRate = (string)value; break;							
						case "TaxAmount": this.str.TaxAmount = (string)value; break;							
						case "AmountOfDeduction": this.str.AmountOfDeduction = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PkpID":
						
							if (value == null || value is System.Int32)
								this.PkpID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "IsNPWP":
						
							if (value == null || value is System.Boolean)
								this.IsNPWP = (System.Boolean?)value;
							break;
						
						case "LowerLimit":
						
							if (value == null || value is System.Decimal)
								this.LowerLimit = (System.Decimal?)value;
							break;
						
						case "UpperLimit":
						
							if (value == null || value is System.Decimal)
								this.UpperLimit = (System.Decimal?)value;
							break;
						
						case "TaxRate":
						
							if (value == null || value is System.Decimal)
								this.TaxRate = (System.Decimal?)value;
							break;
						
						case "TaxAmount":
						
							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
							break;
						
						case "AmountOfDeduction":
						
							if (value == null || value is System.Decimal)
								this.AmountOfDeduction = (System.Decimal?)value;
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
		/// Maps to Pkp.PkpID
		/// </summary>
		virtual public System.Int32? PkpID
		{
			get
			{
				return base.GetSystemInt32(PkpMetadata.ColumnNames.PkpID);
			}
			
			set
			{
				base.SetSystemInt32(PkpMetadata.ColumnNames.PkpID, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PkpMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(PkpMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.IsNPWP
		/// </summary>
		virtual public System.Boolean? IsNPWP
		{
			get
			{
				return base.GetSystemBoolean(PkpMetadata.ColumnNames.IsNPWP);
			}
			
			set
			{
				base.SetSystemBoolean(PkpMetadata.ColumnNames.IsNPWP, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.LowerLimit
		/// </summary>
		virtual public System.Decimal? LowerLimit
		{
			get
			{
				return base.GetSystemDecimal(PkpMetadata.ColumnNames.LowerLimit);
			}
			
			set
			{
				base.SetSystemDecimal(PkpMetadata.ColumnNames.LowerLimit, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.UpperLimit
		/// </summary>
		virtual public System.Decimal? UpperLimit
		{
			get
			{
				return base.GetSystemDecimal(PkpMetadata.ColumnNames.UpperLimit);
			}
			
			set
			{
				base.SetSystemDecimal(PkpMetadata.ColumnNames.UpperLimit, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.TaxRate
		/// </summary>
		virtual public System.Decimal? TaxRate
		{
			get
			{
				return base.GetSystemDecimal(PkpMetadata.ColumnNames.TaxRate);
			}
			
			set
			{
				base.SetSystemDecimal(PkpMetadata.ColumnNames.TaxRate, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.TaxAmount
		/// </summary>
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(PkpMetadata.ColumnNames.TaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(PkpMetadata.ColumnNames.TaxAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.AmountOfDeduction
		/// </summary>
		virtual public System.Decimal? AmountOfDeduction
		{
			get
			{
				return base.GetSystemDecimal(PkpMetadata.ColumnNames.AmountOfDeduction);
			}
			
			set
			{
				base.SetSystemDecimal(PkpMetadata.ColumnNames.AmountOfDeduction, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PkpMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PkpMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Pkp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PkpMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PkpMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPkp entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PkpID
			{
				get
				{
					System.Int32? data = entity.PkpID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PkpID = null;
					else entity.PkpID = Convert.ToInt32(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsNPWP
			{
				get
				{
					System.Boolean? data = entity.IsNPWP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNPWP = null;
					else entity.IsNPWP = Convert.ToBoolean(value);
				}
			}
				
			public System.String LowerLimit
			{
				get
				{
					System.Decimal? data = entity.LowerLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LowerLimit = null;
					else entity.LowerLimit = Convert.ToDecimal(value);
				}
			}
				
			public System.String UpperLimit
			{
				get
				{
					System.Decimal? data = entity.UpperLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpperLimit = null;
					else entity.UpperLimit = Convert.ToDecimal(value);
				}
			}
				
			public System.String TaxRate
			{
				get
				{
					System.Decimal? data = entity.TaxRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxRate = null;
					else entity.TaxRate = Convert.ToDecimal(value);
				}
			}
				
			public System.String TaxAmount
			{
				get
				{
					System.Decimal? data = entity.TaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmount = null;
					else entity.TaxAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String AmountOfDeduction
			{
				get
				{
					System.Decimal? data = entity.AmountOfDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOfDeduction = null;
					else entity.AmountOfDeduction = Convert.ToDecimal(value);
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
			

			private esPkp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPkpQuery query)
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
				throw new Exception("esPkp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Pkp : esPkp
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
	abstract public class esPkpQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PkpMetadata.Meta();
			}
		}	
		

		public esQueryItem PkpID
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.PkpID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsNPWP
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.IsNPWP, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LowerLimit
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.LowerLimit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem UpperLimit
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.UpperLimit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TaxRate
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.TaxRate, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TaxAmount
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AmountOfDeduction
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.AmountOfDeduction, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PkpMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PkpCollection")]
	public partial class PkpCollection : esPkpCollection, IEnumerable<Pkp>
	{
		public PkpCollection()
		{

		}
		
		public static implicit operator List<Pkp>(PkpCollection coll)
		{
			List<Pkp> list = new List<Pkp>();
			
			foreach (Pkp emp in coll)
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
				return  PkpMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PkpQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Pkp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Pkp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PkpQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PkpQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PkpQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Pkp AddNew()
		{
			Pkp entity = base.AddNewEntity() as Pkp;
			
			return entity;
		}

		public Pkp FindByPrimaryKey(System.Int32 pkpID)
		{
			return base.FindByPrimaryKey(pkpID) as Pkp;
		}


		#region IEnumerable<Pkp> Members

		IEnumerator<Pkp> IEnumerable<Pkp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Pkp;
			}
		}

		#endregion
		
		private PkpQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Pkp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Pkp ({PkpID})")]
	[Serializable]
	public partial class Pkp : esPkp
	{
		public Pkp()
		{

		}
	
		public Pkp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PkpMetadata.Meta();
			}
		}
		
		
		
		override protected esPkpQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PkpQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PkpQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PkpQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PkpQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PkpQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PkpQuery : esPkpQuery
	{
		public PkpQuery()
		{

		}		
		
		public PkpQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PkpQuery";
        }
		
			
	}


	[Serializable]
	public partial class PkpMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PkpMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PkpMetadata.ColumnNames.PkpID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PkpMetadata.PropertyNames.PkpID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.ValidFrom, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PkpMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.IsNPWP, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PkpMetadata.PropertyNames.IsNPWP;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.LowerLimit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PkpMetadata.PropertyNames.LowerLimit;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.UpperLimit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PkpMetadata.PropertyNames.UpperLimit;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.TaxRate, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PkpMetadata.PropertyNames.TaxRate;
			c.NumericPrecision = 4;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.TaxAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PkpMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.AmountOfDeduction, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PkpMetadata.PropertyNames.AmountOfDeduction;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PkpMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PkpMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PkpMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PkpMetadata Meta()
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
			 public const string PkpID = "PkpID";
			 public const string ValidFrom = "ValidFrom";
			 public const string IsNPWP = "IsNPWP";
			 public const string LowerLimit = "LowerLimit";
			 public const string UpperLimit = "UpperLimit";
			 public const string TaxRate = "TaxRate";
			 public const string TaxAmount = "TaxAmount";
			 public const string AmountOfDeduction = "AmountOfDeduction";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PkpID = "PkpID";
			 public const string ValidFrom = "ValidFrom";
			 public const string IsNPWP = "IsNPWP";
			 public const string LowerLimit = "LowerLimit";
			 public const string UpperLimit = "UpperLimit";
			 public const string TaxRate = "TaxRate";
			 public const string TaxAmount = "TaxAmount";
			 public const string AmountOfDeduction = "AmountOfDeduction";
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
			lock (typeof(PkpMetadata))
			{
				if(PkpMetadata.mapDelegates == null)
				{
					PkpMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PkpMetadata.meta == null)
				{
					PkpMetadata.meta = new PkpMetadata();
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
				

				meta.AddTypeMap("PkpID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsNPWP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LowerLimit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("UpperLimit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TaxRate", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("AmountOfDeduction", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Pkp";
				meta.Destination = "Pkp";
				
				meta.spInsert = "proc_PkpInsert";				
				meta.spUpdate = "proc_PkpUpdate";		
				meta.spDelete = "proc_PkpDelete";
				meta.spLoadAll = "proc_PkpLoadAll";
				meta.spLoadByPrimaryKey = "proc_PkpLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PkpMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
