/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:15 PM
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
	abstract public class esEmployeeSalaryMatrixCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSalaryMatrixCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeSalaryMatrixCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSalaryMatrixQuery query)
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
			this.InitQuery(query as esEmployeeSalaryMatrixQuery);
		}
		#endregion
		
		virtual public EmployeeSalaryMatrix DetachEntity(EmployeeSalaryMatrix entity)
		{
			return base.DetachEntity(entity) as EmployeeSalaryMatrix;
		}
		
		virtual public EmployeeSalaryMatrix AttachEntity(EmployeeSalaryMatrix entity)
		{
			return base.AttachEntity(entity) as EmployeeSalaryMatrix;
		}
		
		virtual public void Combine(EmployeeSalaryMatrixCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeSalaryMatrix this[int index]
		{
			get
			{
				return base[index] as EmployeeSalaryMatrix;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSalaryMatrix);
		}
	}



	[Serializable]
	abstract public class esEmployeeSalaryMatrix : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSalaryMatrixQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSalaryMatrix()
		{

		}

		public esEmployeeSalaryMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 employeeSalaryMatrixID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeSalaryMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeSalaryMatrixID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 employeeSalaryMatrixID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeSalaryMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeSalaryMatrixID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 employeeSalaryMatrixID)
		{
			esEmployeeSalaryMatrixQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeSalaryMatrixID == employeeSalaryMatrixID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 employeeSalaryMatrixID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeSalaryMatrixID",employeeSalaryMatrixID);
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
						case "EmployeeSalaryMatrixID": this.str.EmployeeSalaryMatrixID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;							
						case "Qty": this.str.Qty = (string)value; break;							
						case "NominalAmount": this.str.NominalAmount = (string)value; break;							
						case "SRCurrencyCode": this.str.SRCurrencyCode = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeSalaryMatrixID":
						
							if (value == null || value is System.Int64)
								this.EmployeeSalaryMatrixID = (System.Int64?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "SalaryComponentID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						
						case "Qty":
						
							if (value == null || value is System.Int32)
								this.Qty = (System.Int32?)value;
							break;
						
						case "NominalAmount":
						
							if (value == null || value is System.Decimal)
								this.NominalAmount = (System.Decimal?)value;
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
		/// Maps to EmployeeSalaryMatrix.EmployeeSalaryMatrixID
		/// </summary>
		virtual public System.Int64? EmployeeSalaryMatrixID
		{
			get
			{
				return base.GetSystemInt64(EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID);
			}
			
			set
			{
				base.SetSystemInt64(EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.SalaryComponentID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.Qty
		/// </summary>
		virtual public System.Int32? Qty
		{
			get
			{
				return base.GetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeSalaryMatrixMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.NominalAmount
		/// </summary>
		virtual public System.Decimal? NominalAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeSalaryMatrixMetadata.ColumnNames.NominalAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeSalaryMatrixMetadata.ColumnNames.NominalAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.SRCurrencyCode
		/// </summary>
		virtual public System.String SRCurrencyCode
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryMatrixMetadata.ColumnNames.SRCurrencyCode);
			}
			
			set
			{
				base.SetSystemString(EmployeeSalaryMatrixMetadata.ColumnNames.SRCurrencyCode, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeSalaryMatrix.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSalaryMatrix entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeSalaryMatrixID
			{
				get
				{
					System.Int64? data = entity.EmployeeSalaryMatrixID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeSalaryMatrixID = null;
					else entity.EmployeeSalaryMatrixID = Convert.ToInt64(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
				}
			}
				
			public System.String Qty
			{
				get
				{
					System.Int32? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToInt32(value);
				}
			}
				
			public System.String NominalAmount
			{
				get
				{
					System.Decimal? data = entity.NominalAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NominalAmount = null;
					else entity.NominalAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRCurrencyCode
			{
				get
				{
					System.String data = entity.SRCurrencyCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrencyCode = null;
					else entity.SRCurrencyCode = Convert.ToString(value);
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
			

			private esEmployeeSalaryMatrix entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSalaryMatrixQuery query)
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
				throw new Exception("esEmployeeSalaryMatrix can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeSalaryMatrix : esEmployeeSalaryMatrix
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
	abstract public class esEmployeeSalaryMatrixQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSalaryMatrixMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeSalaryMatrixID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.Qty, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NominalAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.NominalAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRCurrencyCode
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.SRCurrencyCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSalaryMatrixCollection")]
	public partial class EmployeeSalaryMatrixCollection : esEmployeeSalaryMatrixCollection, IEnumerable<EmployeeSalaryMatrix>
	{
		public EmployeeSalaryMatrixCollection()
		{

		}
		
		public static implicit operator List<EmployeeSalaryMatrix>(EmployeeSalaryMatrixCollection coll)
		{
			List<EmployeeSalaryMatrix> list = new List<EmployeeSalaryMatrix>();
			
			foreach (EmployeeSalaryMatrix emp in coll)
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
				return  EmployeeSalaryMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSalaryMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSalaryMatrix(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSalaryMatrix();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeSalaryMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSalaryMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeSalaryMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeSalaryMatrix AddNew()
		{
			EmployeeSalaryMatrix entity = base.AddNewEntity() as EmployeeSalaryMatrix;
			
			return entity;
		}

		public EmployeeSalaryMatrix FindByPrimaryKey(System.Int64 employeeSalaryMatrixID)
		{
			return base.FindByPrimaryKey(employeeSalaryMatrixID) as EmployeeSalaryMatrix;
		}


		#region IEnumerable<EmployeeSalaryMatrix> Members

		IEnumerator<EmployeeSalaryMatrix> IEnumerable<EmployeeSalaryMatrix>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSalaryMatrix;
			}
		}

		#endregion
		
		private EmployeeSalaryMatrixQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSalaryMatrix' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeSalaryMatrix ({EmployeeSalaryMatrixID})")]
	[Serializable]
	public partial class EmployeeSalaryMatrix : esEmployeeSalaryMatrix
	{
		public EmployeeSalaryMatrix()
		{

		}
	
		public EmployeeSalaryMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSalaryMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeSalaryMatrixQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSalaryMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeSalaryMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSalaryMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeSalaryMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeSalaryMatrixQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeSalaryMatrixQuery : esEmployeeSalaryMatrixQuery
	{
		public EmployeeSalaryMatrixQuery()
		{

		}		
		
		public EmployeeSalaryMatrixQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeSalaryMatrixQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeSalaryMatrixMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSalaryMatrixMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.EmployeeSalaryMatrixID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.EmployeeSalaryMatrixID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.SalaryComponentID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.Qty, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.NominalAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.NominalAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.SRCurrencyCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.SRCurrencyCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeSalaryMatrixMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryMatrixMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeSalaryMatrixMetadata Meta()
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
			 public const string EmployeeSalaryMatrixID = "EmployeeSalaryMatrixID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string Qty = "Qty";
			 public const string NominalAmount = "NominalAmount";
			 public const string SRCurrencyCode = "SRCurrencyCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeSalaryMatrixID = "EmployeeSalaryMatrixID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string Qty = "Qty";
			 public const string NominalAmount = "NominalAmount";
			 public const string SRCurrencyCode = "SRCurrencyCode";
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
			lock (typeof(EmployeeSalaryMatrixMetadata))
			{
				if(EmployeeSalaryMatrixMetadata.mapDelegates == null)
				{
					EmployeeSalaryMatrixMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeSalaryMatrixMetadata.meta == null)
				{
					EmployeeSalaryMatrixMetadata.meta = new EmployeeSalaryMatrixMetadata();
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
				

				meta.AddTypeMap("EmployeeSalaryMatrixID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Qty", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NominalAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("SRCurrencyCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeSalaryMatrix";
				meta.Destination = "EmployeeSalaryMatrix";
				
				meta.spInsert = "proc_EmployeeSalaryMatrixInsert";				
				meta.spUpdate = "proc_EmployeeSalaryMatrixUpdate";		
				meta.spDelete = "proc_EmployeeSalaryMatrixDelete";
				meta.spLoadAll = "proc_EmployeeSalaryMatrixLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSalaryMatrixLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSalaryMatrixMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
