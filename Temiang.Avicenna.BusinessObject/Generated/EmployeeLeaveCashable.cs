/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esEmployeeLeaveCashableCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLeaveCashableCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeLeaveCashableCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveCashableQuery query)
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
			this.InitQuery(query as esEmployeeLeaveCashableQuery);
		}
		#endregion
		
		virtual public EmployeeLeaveCashable DetachEntity(EmployeeLeaveCashable entity)
		{
			return base.DetachEntity(entity) as EmployeeLeaveCashable;
		}
		
		virtual public EmployeeLeaveCashable AttachEntity(EmployeeLeaveCashable entity)
		{
			return base.AttachEntity(entity) as EmployeeLeaveCashable;
		}
		
		virtual public void Combine(EmployeeLeaveCashableCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeLeaveCashable this[int index]
		{
			get
			{
				return base[index] as EmployeeLeaveCashable;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLeaveCashable);
		}
	}



	[Serializable]
	abstract public class esEmployeeLeaveCashable : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLeaveCashableQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLeaveCashable()
		{

		}

		public esEmployeeLeaveCashable(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeLeaveCashableID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLeaveCashableID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLeaveCashableID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeLeaveCashableID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLeaveCashableID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLeaveCashableID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeLeaveCashableID)
		{
			esEmployeeLeaveCashableQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeLeaveCashableID == employeeLeaveCashableID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeLeaveCashableID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeLeaveCashableID",employeeLeaveCashableID);
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
						case "EmployeeLeaveCashableID": this.str.EmployeeLeaveCashableID = (string)value; break;							
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;							
						case "TotalDay": this.str.TotalDay = (string)value; break;							
						case "LetterNumber": this.str.LetterNumber = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeLeaveCashableID":
						
							if (value == null || value is System.Int32)
								this.EmployeeLeaveCashableID = (System.Int32?)value;
							break;
						
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "SalaryComponentID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						
						case "TotalDay":
						
							if (value == null || value is System.Int32)
								this.TotalDay = (System.Int32?)value;
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
		/// Maps to EmployeeLeaveCashable.EmployeeLeaveCashableID
		/// </summary>
		virtual public System.Int32? EmployeeLeaveCashableID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.EmployeeLeaveCashableID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.EmployeeLeaveCashableID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.SalaryComponentID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.TotalDay
		/// </summary>
		virtual public System.Int32? TotalDay
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.TotalDay);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLeaveCashableMetadata.ColumnNames.TotalDay, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.LetterNumber
		/// </summary>
		virtual public System.String LetterNumber
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveCashableMetadata.ColumnNames.LetterNumber);
			}
			
			set
			{
				base.SetSystemString(EmployeeLeaveCashableMetadata.ColumnNames.LetterNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLeaveCashable.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeLeaveCashable entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeLeaveCashableID
			{
				get
				{
					System.Int32? data = entity.EmployeeLeaveCashableID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLeaveCashableID = null;
					else entity.EmployeeLeaveCashableID = Convert.ToInt32(value);
				}
			}
				
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
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
				
			public System.String TotalDay
			{
				get
				{
					System.Int32? data = entity.TotalDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalDay = null;
					else entity.TotalDay = Convert.ToInt32(value);
				}
			}
				
			public System.String LetterNumber
			{
				get
				{
					System.String data = entity.LetterNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterNumber = null;
					else entity.LetterNumber = Convert.ToString(value);
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
			

			private esEmployeeLeaveCashable entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveCashableQuery query)
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
				throw new Exception("esEmployeeLeaveCashable can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeLeaveCashable : esEmployeeLeaveCashable
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
	abstract public class esEmployeeLeaveCashableQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveCashableMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeLeaveCashableID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.EmployeeLeaveCashableID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalDay
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.TotalDay, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LetterNumber
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.LetterNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLeaveCashableCollection")]
	public partial class EmployeeLeaveCashableCollection : esEmployeeLeaveCashableCollection, IEnumerable<EmployeeLeaveCashable>
	{
		public EmployeeLeaveCashableCollection()
		{

		}
		
		public static implicit operator List<EmployeeLeaveCashable>(EmployeeLeaveCashableCollection coll)
		{
			List<EmployeeLeaveCashable> list = new List<EmployeeLeaveCashable>();
			
			foreach (EmployeeLeaveCashable emp in coll)
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
				return  EmployeeLeaveCashableMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveCashableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLeaveCashable(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLeaveCashable();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeLeaveCashableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveCashableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeLeaveCashableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeLeaveCashable AddNew()
		{
			EmployeeLeaveCashable entity = base.AddNewEntity() as EmployeeLeaveCashable;
			
			return entity;
		}

		public EmployeeLeaveCashable FindByPrimaryKey(System.Int32 employeeLeaveCashableID)
		{
			return base.FindByPrimaryKey(employeeLeaveCashableID) as EmployeeLeaveCashable;
		}


		#region IEnumerable<EmployeeLeaveCashable> Members

		IEnumerator<EmployeeLeaveCashable> IEnumerable<EmployeeLeaveCashable>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLeaveCashable;
			}
		}

		#endregion
		
		private EmployeeLeaveCashableQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLeaveCashable' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeLeaveCashable ({EmployeeLeaveCashableID})")]
	[Serializable]
	public partial class EmployeeLeaveCashable : esEmployeeLeaveCashable
	{
		public EmployeeLeaveCashable()
		{

		}
	
		public EmployeeLeaveCashable(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveCashableMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeLeaveCashableQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveCashableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeLeaveCashableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveCashableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeLeaveCashableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeLeaveCashableQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeLeaveCashableQuery : esEmployeeLeaveCashableQuery
	{
		public EmployeeLeaveCashableQuery()
		{

		}		
		
		public EmployeeLeaveCashableQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeLeaveCashableQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeLeaveCashableMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLeaveCashableMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.EmployeeLeaveCashableID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.EmployeeLeaveCashableID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.PayrollPeriodID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.SalaryComponentID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.TotalDay, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.TotalDay;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.LetterNumber, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.LetterNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLeaveCashableMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveCashableMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeLeaveCashableMetadata Meta()
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
			 public const string EmployeeLeaveCashableID = "EmployeeLeaveCashableID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string TotalDay = "TotalDay";
			 public const string LetterNumber = "LetterNumber";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeLeaveCashableID = "EmployeeLeaveCashableID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string PersonID = "PersonID";
			 public const string SalaryComponentID = "SalaryComponentID";
			 public const string TotalDay = "TotalDay";
			 public const string LetterNumber = "LetterNumber";
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
			lock (typeof(EmployeeLeaveCashableMetadata))
			{
				if(EmployeeLeaveCashableMetadata.mapDelegates == null)
				{
					EmployeeLeaveCashableMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeLeaveCashableMetadata.meta == null)
				{
					EmployeeLeaveCashableMetadata.meta = new EmployeeLeaveCashableMetadata();
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
				

				meta.AddTypeMap("EmployeeLeaveCashableID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalDay", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LetterNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeLeaveCashable";
				meta.Destination = "EmployeeLeaveCashable";
				
				meta.spInsert = "proc_EmployeeLeaveCashableInsert";				
				meta.spUpdate = "proc_EmployeeLeaveCashableUpdate";		
				meta.spDelete = "proc_EmployeeLeaveCashableDelete";
				meta.spLoadAll = "proc_EmployeeLeaveCashableLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLeaveCashableLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLeaveCashableMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
