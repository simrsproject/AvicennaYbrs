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
	abstract public class esEmployeeGradeCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeGradeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeGradeCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeGradeQuery query)
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
			this.InitQuery(query as esEmployeeGradeQuery);
		}
		#endregion
		
		virtual public EmployeeGrade DetachEntity(EmployeeGrade entity)
		{
			return base.DetachEntity(entity) as EmployeeGrade;
		}
		
		virtual public EmployeeGrade AttachEntity(EmployeeGrade entity)
		{
			return base.AttachEntity(entity) as EmployeeGrade;
		}
		
		virtual public void Combine(EmployeeGradeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeGrade this[int index]
		{
			get
			{
				return base[index] as EmployeeGrade;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeGrade);
		}
	}



	[Serializable]
	abstract public class esEmployeeGrade : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeGradeQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeGrade()
		{

		}

		public esEmployeeGrade(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeGradeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeGradeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeGradeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeGradeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeGradeID)
		{
			esEmployeeGradeQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeGradeID == employeeGradeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeGradeID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeGradeID",employeeGradeID);
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
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "EmployeeGradeMasterID": this.str.EmployeeGradeMasterID = (string)value; break;							
						case "SalaryTableNumber": this.str.SalaryTableNumber = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeGradeID":
						
							if (value == null || value is System.Int32)
								this.EmployeeGradeID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "EmployeeGradeMasterID":
						
							if (value == null || value is System.Int32)
								this.EmployeeGradeMasterID = (System.Int32?)value;
							break;
						
						case "SalaryTableNumber":
						
							if (value == null || value is System.Int32)
								this.SalaryTableNumber = (System.Int32?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to EmployeeGrade.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMetadata.ColumnNames.EmployeeGradeID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.EmployeeGradeMasterID
		/// </summary>
		virtual public System.Int32? EmployeeGradeMasterID
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMetadata.ColumnNames.EmployeeGradeMasterID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMetadata.ColumnNames.EmployeeGradeMasterID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.SalaryTableNumber
		/// </summary>
		virtual public System.Int32? SalaryTableNumber
		{
			get
			{
				return base.GetSystemInt32(EmployeeGradeMetadata.ColumnNames.SalaryTableNumber);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeGradeMetadata.ColumnNames.SalaryTableNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeGradeMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeGradeMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeGradeMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeGradeMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeGradeMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeGradeMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeGradeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeGradeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeGrade.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeGradeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeGradeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeGrade entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeGradeID
			{
				get
				{
					System.Int32? data = entity.EmployeeGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeID = null;
					else entity.EmployeeGradeID = Convert.ToInt32(value);
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
				
			public System.String EmployeeGradeMasterID
			{
				get
				{
					System.Int32? data = entity.EmployeeGradeMasterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeMasterID = null;
					else entity.EmployeeGradeMasterID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryTableNumber
			{
				get
				{
					System.Int32? data = entity.SalaryTableNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryTableNumber = null;
					else entity.SalaryTableNumber = Convert.ToInt32(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			

			private esEmployeeGrade entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeGradeQuery query)
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
				throw new Exception("esEmployeeGrade can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeGrade : esEmployeeGrade
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
	abstract public class esEmployeeGradeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeGradeMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeGradeMasterID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.EmployeeGradeMasterID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.SalaryTableNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeGradeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeGradeCollection")]
	public partial class EmployeeGradeCollection : esEmployeeGradeCollection, IEnumerable<EmployeeGrade>
	{
		public EmployeeGradeCollection()
		{

		}
		
		public static implicit operator List<EmployeeGrade>(EmployeeGradeCollection coll)
		{
			List<EmployeeGrade> list = new List<EmployeeGrade>();
			
			foreach (EmployeeGrade emp in coll)
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
				return  EmployeeGradeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeGrade(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeGrade();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeGradeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeGrade AddNew()
		{
			EmployeeGrade entity = base.AddNewEntity() as EmployeeGrade;
			
			return entity;
		}

		public EmployeeGrade FindByPrimaryKey(System.Int32 employeeGradeID)
		{
			return base.FindByPrimaryKey(employeeGradeID) as EmployeeGrade;
		}


		#region IEnumerable<EmployeeGrade> Members

		IEnumerator<EmployeeGrade> IEnumerable<EmployeeGrade>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeGrade;
			}
		}

		#endregion
		
		private EmployeeGradeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeGrade' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeGrade ({EmployeeGradeID})")]
	[Serializable]
	public partial class EmployeeGrade : esEmployeeGrade
	{
		public EmployeeGrade()
		{

		}
	
		public EmployeeGrade(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeGradeMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeGradeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeGradeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeGradeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeGradeQuery : esEmployeeGradeQuery
	{
		public EmployeeGradeQuery()
		{

		}		
		
		public EmployeeGradeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeGradeQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeGradeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeGradeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.EmployeeGradeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.EmployeeGradeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.EmployeeGradeMasterID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.EmployeeGradeMasterID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.SalaryTableNumber, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.SalaryTableNumber;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.ValidFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.ValidTo, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeGradeMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeGradeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeGradeMetadata Meta()
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
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string PersonID = "PersonID";
			 public const string EmployeeGradeMasterID = "EmployeeGradeMasterID";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string IsActive = "IsActive";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string PersonID = "PersonID";
			 public const string EmployeeGradeMasterID = "EmployeeGradeMasterID";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string IsActive = "IsActive";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
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
			lock (typeof(EmployeeGradeMetadata))
			{
				if(EmployeeGradeMetadata.mapDelegates == null)
				{
					EmployeeGradeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeGradeMetadata.meta == null)
				{
					EmployeeGradeMetadata.meta = new EmployeeGradeMetadata();
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
				

				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeMasterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryTableNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeGrade";
				meta.Destination = "EmployeeGrade";
				
				meta.spInsert = "proc_EmployeeGradeInsert";				
				meta.spUpdate = "proc_EmployeeGradeUpdate";		
				meta.spDelete = "proc_EmployeeGradeDelete";
				meta.spLoadAll = "proc_EmployeeGradeLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeGradeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeGradeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
