/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:13 PM
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
	abstract public class esDepartmentCollection : esEntityCollectionWAuditLog
	{
		public esDepartmentCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DepartmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esDepartmentQuery query)
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
			this.InitQuery(query as esDepartmentQuery);
		}
		#endregion
		
		virtual public Department DetachEntity(Department entity)
		{
			return base.DetachEntity(entity) as Department;
		}
		
		virtual public Department AttachEntity(Department entity)
		{
			return base.AttachEntity(entity) as Department;
		}
		
		virtual public void Combine(DepartmentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Department this[int index]
		{
			get
			{
				return base[index] as Department;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Department);
		}
	}



	[Serializable]
	abstract public class esDepartment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDepartmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esDepartment()
		{

		}

		public esDepartment(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String departmentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(departmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(departmentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String departmentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(departmentID);
			else
				return LoadByPrimaryKeyStoredProcedure(departmentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String departmentID)
		{
			esDepartmentQuery query = this.GetDynamicQuery();
			query.Where(query.DepartmentID == departmentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String departmentID)
		{
			esParameters parms = new esParameters();
			parms.Add("DepartmentID",departmentID);
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
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "DepartmentName": this.str.DepartmentName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "Initial": this.str.Initial = (string)value; break;							
						case "DepartmentManager": this.str.DepartmentManager = (string)value; break;							
						case "IsHasRegistration": this.str.IsHasRegistration = (string)value; break;							
						case "IsAllowBackDateRegistration": this.str.IsAllowBackDateRegistration = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsHasRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsHasRegistration = (System.Boolean?)value;
							break;
						
						case "IsAllowBackDateRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsAllowBackDateRegistration = (System.Boolean?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to Department.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.DepartmentName
		/// </summary>
		virtual public System.String DepartmentName
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.DepartmentName);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.DepartmentName, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.Initial
		/// </summary>
		virtual public System.String Initial
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.Initial);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.Initial, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.DepartmentManager
		/// </summary>
		virtual public System.String DepartmentManager
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.DepartmentManager);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.DepartmentManager, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.IsHasRegistration
		/// </summary>
		virtual public System.Boolean? IsHasRegistration
		{
			get
			{
				return base.GetSystemBoolean(DepartmentMetadata.ColumnNames.IsHasRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(DepartmentMetadata.ColumnNames.IsHasRegistration, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.IsAllowBackDateRegistration
		/// </summary>
		virtual public System.Boolean? IsAllowBackDateRegistration
		{
			get
			{
				return base.GetSystemBoolean(DepartmentMetadata.ColumnNames.IsAllowBackDateRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(DepartmentMetadata.ColumnNames.IsAllowBackDateRegistration, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DepartmentMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(DepartmentMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DepartmentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DepartmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Department.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DepartmentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DepartmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDepartment entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentName
			{
				get
				{
					System.String data = entity.DepartmentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentName = null;
					else entity.DepartmentName = Convert.ToString(value);
				}
			}
				
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
				
			public System.String Initial
			{
				get
				{
					System.String data = entity.Initial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Initial = null;
					else entity.Initial = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentManager
			{
				get
				{
					System.String data = entity.DepartmentManager;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentManager = null;
					else entity.DepartmentManager = Convert.ToString(value);
				}
			}
				
			public System.String IsHasRegistration
			{
				get
				{
					System.Boolean? data = entity.IsHasRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHasRegistration = null;
					else entity.IsHasRegistration = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAllowBackDateRegistration
			{
				get
				{
					System.Boolean? data = entity.IsAllowBackDateRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowBackDateRegistration = null;
					else entity.IsAllowBackDateRegistration = Convert.ToBoolean(value);
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
			

			private esDepartment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDepartmentQuery query)
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
				throw new Exception("esDepartment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Department : esDepartment
	{

				
		#region RegistrationCollectionByDepartmentID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefRegistrationToDepartment
		/// </summary>

		[XmlIgnore]
		public RegistrationCollection RegistrationCollectionByDepartmentID
		{
			get
			{
				if(this._RegistrationCollectionByDepartmentID == null)
				{
					this._RegistrationCollectionByDepartmentID = new RegistrationCollection();
					this._RegistrationCollectionByDepartmentID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("RegistrationCollectionByDepartmentID", this._RegistrationCollectionByDepartmentID);
				
					if(this.DepartmentID != null)
					{
						this._RegistrationCollectionByDepartmentID.Query.Where(this._RegistrationCollectionByDepartmentID.Query.DepartmentID == this.DepartmentID);
						this._RegistrationCollectionByDepartmentID.Query.Load();

						// Auto-hookup Foreign Keys
						this._RegistrationCollectionByDepartmentID.fks.Add(RegistrationMetadata.ColumnNames.DepartmentID, this.DepartmentID);
					}
				}

				return this._RegistrationCollectionByDepartmentID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._RegistrationCollectionByDepartmentID != null) 
				{ 
					this.RemovePostSave("RegistrationCollectionByDepartmentID"); 
					this._RegistrationCollectionByDepartmentID = null;
					
				} 
			} 			
		}

		private RegistrationCollection _RegistrationCollectionByDepartmentID;
		#endregion

				
		#region ServiceUnitCollectionByDepartmentID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefServiceUnitToDepartment
		/// </summary>

		[XmlIgnore]
		public ServiceUnitCollection ServiceUnitCollectionByDepartmentID
		{
			get
			{
				if(this._ServiceUnitCollectionByDepartmentID == null)
				{
					this._ServiceUnitCollectionByDepartmentID = new ServiceUnitCollection();
					this._ServiceUnitCollectionByDepartmentID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ServiceUnitCollectionByDepartmentID", this._ServiceUnitCollectionByDepartmentID);
				
					if(this.DepartmentID != null)
					{
						this._ServiceUnitCollectionByDepartmentID.Query.Where(this._ServiceUnitCollectionByDepartmentID.Query.DepartmentID == this.DepartmentID);
						this._ServiceUnitCollectionByDepartmentID.Query.Load();

						// Auto-hookup Foreign Keys
						this._ServiceUnitCollectionByDepartmentID.fks.Add(ServiceUnitMetadata.ColumnNames.DepartmentID, this.DepartmentID);
					}
				}

				return this._ServiceUnitCollectionByDepartmentID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ServiceUnitCollectionByDepartmentID != null) 
				{ 
					this.RemovePostSave("ServiceUnitCollectionByDepartmentID"); 
					this._ServiceUnitCollectionByDepartmentID = null;
					
				} 
			} 			
		}

		private ServiceUnitCollection _ServiceUnitCollectionByDepartmentID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "RegistrationCollectionByDepartmentID", typeof(RegistrationCollection), new Registration()));
			props.Add(new esPropertyDescriptor(this, "ServiceUnitCollectionByDepartmentID", typeof(ServiceUnitCollection), new ServiceUnit()));
		
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
	abstract public class esDepartmentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DepartmentMetadata.Meta();
			}
		}	
		

		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentName
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.DepartmentName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem Initial
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.Initial, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentManager
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.DepartmentManager, esSystemType.String);
			}
		} 
		
		public esQueryItem IsHasRegistration
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.IsHasRegistration, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAllowBackDateRegistration
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.IsAllowBackDateRegistration, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DepartmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DepartmentCollection")]
	public partial class DepartmentCollection : esDepartmentCollection, IEnumerable<Department>
	{
		public DepartmentCollection()
		{

		}
		
		public static implicit operator List<Department>(DepartmentCollection coll)
		{
			List<Department> list = new List<Department>();
			
			foreach (Department emp in coll)
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
				return  DepartmentMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DepartmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Department(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Department();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DepartmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DepartmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DepartmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Department AddNew()
		{
			Department entity = base.AddNewEntity() as Department;
			
			return entity;
		}

		public Department FindByPrimaryKey(System.String departmentID)
		{
			return base.FindByPrimaryKey(departmentID) as Department;
		}


		#region IEnumerable<Department> Members

		IEnumerator<Department> IEnumerable<Department>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Department;
			}
		}

		#endregion
		
		private DepartmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Department' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Department ({DepartmentID})")]
	[Serializable]
	public partial class Department : esDepartment
	{
		public Department()
		{

		}
	
		public Department(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DepartmentMetadata.Meta();
			}
		}
		
		
		
		override protected esDepartmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DepartmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DepartmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DepartmentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DepartmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DepartmentQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DepartmentQuery : esDepartmentQuery
	{
		public DepartmentQuery()
		{

		}		
		
		public DepartmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DepartmentQuery";
        }
		
			
	}


	[Serializable]
	public partial class DepartmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DepartmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.DepartmentID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.DepartmentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.DepartmentName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.DepartmentName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.Initial, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.Initial;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.DepartmentManager, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.DepartmentManager;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.IsHasRegistration, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DepartmentMetadata.PropertyNames.IsHasRegistration;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.IsAllowBackDateRegistration, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DepartmentMetadata.PropertyNames.IsAllowBackDateRegistration;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DepartmentMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DepartmentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DepartmentMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DepartmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DepartmentMetadata Meta()
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
			 public const string DepartmentID = "DepartmentID";
			 public const string DepartmentName = "DepartmentName";
			 public const string ShortName = "ShortName";
			 public const string Initial = "Initial";
			 public const string DepartmentManager = "DepartmentManager";
			 public const string IsHasRegistration = "IsHasRegistration";
			 public const string IsAllowBackDateRegistration = "IsAllowBackDateRegistration";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DepartmentID = "DepartmentID";
			 public const string DepartmentName = "DepartmentName";
			 public const string ShortName = "ShortName";
			 public const string Initial = "Initial";
			 public const string DepartmentManager = "DepartmentManager";
			 public const string IsHasRegistration = "IsHasRegistration";
			 public const string IsAllowBackDateRegistration = "IsAllowBackDateRegistration";
			 public const string IsActive = "IsActive";
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
			lock (typeof(DepartmentMetadata))
			{
				if(DepartmentMetadata.mapDelegates == null)
				{
					DepartmentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DepartmentMetadata.meta == null)
				{
					DepartmentMetadata.meta = new DepartmentMetadata();
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
				

				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Initial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentManager", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHasRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowBackDateRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Department";
				meta.Destination = "Department";
				
				meta.spInsert = "proc_DepartmentInsert";				
				meta.spUpdate = "proc_DepartmentUpdate";		
				meta.spDelete = "proc_DepartmentDelete";
				meta.spLoadAll = "proc_DepartmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_DepartmentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DepartmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
