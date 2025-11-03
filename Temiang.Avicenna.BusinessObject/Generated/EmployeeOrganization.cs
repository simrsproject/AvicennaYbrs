/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/21/2022 10:48:00 AM
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
	abstract public class esEmployeeOrganizationCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeOrganizationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeOrganizationCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeOrganizationQuery query)
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
			this.InitQuery(query as esEmployeeOrganizationQuery);
		}
		#endregion
		
		virtual public EmployeeOrganization DetachEntity(EmployeeOrganization entity)
		{
			return base.DetachEntity(entity) as EmployeeOrganization;
		}
		
		virtual public EmployeeOrganization AttachEntity(EmployeeOrganization entity)
		{
			return base.AttachEntity(entity) as EmployeeOrganization;
		}
		
		virtual public void Combine(EmployeeOrganizationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeOrganization this[int index]
		{
			get
			{
				return base[index] as EmployeeOrganization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeOrganization);
		}
	}



	[Serializable]
	abstract public class esEmployeeOrganization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeOrganizationQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeOrganization()
		{

		}

		public esEmployeeOrganization(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeOrganizationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeOrganizationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeOrganizationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeOrganizationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeOrganizationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeOrganizationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeOrganizationID)
		{
			esEmployeeOrganizationQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeOrganizationID == employeeOrganizationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeOrganizationID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeOrganizationID",employeeOrganizationID);
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
						case "EmployeeOrganizationID": this.str.EmployeeOrganizationID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "OrganizationID": this.str.OrganizationID = (string)value; break;							
						case "SubOrganizationID": this.str.SubOrganizationID = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "SubDivisonID": this.str.SubDivisonID = (string)value; break;							
						case "SROrganizationLevelType": this.str.SROrganizationLevelType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeOrganizationID":
						
							if (value == null || value is System.Int32)
								this.EmployeeOrganizationID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "OrganizationID":
						
							if (value == null || value is System.Int32)
								this.OrganizationID = (System.Int32?)value;
							break;
						
						case "SubOrganizationID":
						
							if (value == null || value is System.Int32)
								this.SubOrganizationID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "SubDivisonID":
						
							if (value == null || value is System.Int32)
								this.SubDivisonID = (System.Int32?)value;
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
		/// Maps to EmployeeOrganization.EmployeeOrganizationID
		/// </summary>
		virtual public System.Int32? EmployeeOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.OrganizationID
		/// </summary>
		virtual public System.Int32? OrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.OrganizationID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.OrganizationID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.SubOrganizationID
		/// </summary>
		virtual public System.Int32? SubOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOrganizationMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeOrganizationMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeOrganizationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOrganizationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeOrganizationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.SubDivisonID
		/// </summary>
		virtual public System.Int32? SubDivisonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.SubDivisonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeOrganizationMetadata.ColumnNames.SubDivisonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeOrganization.SROrganizationLevelType
		/// </summary>
		virtual public System.String SROrganizationLevelType
		{
			get
			{
				return base.GetSystemString(EmployeeOrganizationMetadata.ColumnNames.SROrganizationLevelType);
			}
			
			set
			{
				base.SetSystemString(EmployeeOrganizationMetadata.ColumnNames.SROrganizationLevelType, value);
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
			public esStrings(esEmployeeOrganization entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeOrganizationID
			{
				get
				{
					System.Int32? data = entity.EmployeeOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeOrganizationID = null;
					else entity.EmployeeOrganizationID = Convert.ToInt32(value);
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
				
			public System.String OrganizationID
			{
				get
				{
					System.Int32? data = entity.OrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationID = null;
					else entity.OrganizationID = Convert.ToInt32(value);
				}
			}
				
			public System.String SubOrganizationID
			{
				get
				{
					System.Int32? data = entity.SubOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubOrganizationID = null;
					else entity.SubOrganizationID = Convert.ToInt32(value);
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
				
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String SubDivisonID
			{
				get
				{
					System.Int32? data = entity.SubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubDivisonID = null;
					else entity.SubDivisonID = Convert.ToInt32(value);
				}
			}
				
			public System.String SROrganizationLevelType
			{
				get
				{
					System.String data = entity.SROrganizationLevelType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROrganizationLevelType = null;
					else entity.SROrganizationLevelType = Convert.ToString(value);
				}
			}
			

			private esEmployeeOrganization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeOrganizationQuery query)
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
				throw new Exception("esEmployeeOrganization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esEmployeeOrganizationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOrganizationMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.OrganizationID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem SubDivisonID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.SubDivisonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SROrganizationLevelType
		{
			get
			{
				return new esQueryItem(this, EmployeeOrganizationMetadata.ColumnNames.SROrganizationLevelType, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeOrganizationCollection")]
	public partial class EmployeeOrganizationCollection : esEmployeeOrganizationCollection, IEnumerable<EmployeeOrganization>
	{
		public EmployeeOrganizationCollection()
		{

		}
		
		public static implicit operator List<EmployeeOrganization>(EmployeeOrganizationCollection coll)
		{
			List<EmployeeOrganization> list = new List<EmployeeOrganization>();
			
			foreach (EmployeeOrganization emp in coll)
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
				return  EmployeeOrganizationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOrganizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeOrganization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeOrganization();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeOrganizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOrganizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeOrganizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeOrganization AddNew()
		{
			EmployeeOrganization entity = base.AddNewEntity() as EmployeeOrganization;
			
			return entity;
		}

		public EmployeeOrganization FindByPrimaryKey(System.Int32 employeeOrganizationID)
		{
			return base.FindByPrimaryKey(employeeOrganizationID) as EmployeeOrganization;
		}


		#region IEnumerable<EmployeeOrganization> Members

		IEnumerator<EmployeeOrganization> IEnumerable<EmployeeOrganization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeOrganization;
			}
		}

		#endregion
		
		private EmployeeOrganizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeOrganization' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeOrganization ({EmployeeOrganizationID})")]
	[Serializable]
	public partial class EmployeeOrganization : esEmployeeOrganization
	{
		public EmployeeOrganization()
		{

		}
	
		public EmployeeOrganization(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOrganizationMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeOrganizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOrganizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeOrganizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOrganizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeOrganizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeOrganizationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeOrganizationQuery : esEmployeeOrganizationQuery
	{
		public EmployeeOrganizationQuery()
		{

		}		
		
		public EmployeeOrganizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeOrganizationQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeOrganizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeOrganizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.EmployeeOrganizationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.EmployeeOrganizationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.OrganizationID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.OrganizationID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.SubOrganizationID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.SubOrganizationID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.ValidFrom, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.ValidTo, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.ServiceUnitID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.SubDivisonID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.SubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeOrganizationMetadata.ColumnNames.SROrganizationLevelType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOrganizationMetadata.PropertyNames.SROrganizationLevelType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeOrganizationMetadata Meta()
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
			 public const string EmployeeOrganizationID = "EmployeeOrganizationID";
			 public const string PersonID = "PersonID";
			 public const string OrganizationID = "OrganizationID";
			 public const string SubOrganizationID = "SubOrganizationID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string SubDivisonID = "SubDivisonID";
			 public const string SROrganizationLevelType = "SROrganizationLevelType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeOrganizationID = "EmployeeOrganizationID";
			 public const string PersonID = "PersonID";
			 public const string OrganizationID = "OrganizationID";
			 public const string SubOrganizationID = "SubOrganizationID";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string SubDivisonID = "SubDivisonID";
			 public const string SROrganizationLevelType = "SROrganizationLevelType";
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
			lock (typeof(EmployeeOrganizationMetadata))
			{
				if(EmployeeOrganizationMetadata.mapDelegates == null)
				{
					EmployeeOrganizationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeOrganizationMetadata.meta == null)
				{
					EmployeeOrganizationMetadata.meta = new EmployeeOrganizationMetadata();
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
				

				meta.AddTypeMap("EmployeeOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SROrganizationLevelType", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeOrganization";
				meta.Destination = "EmployeeOrganization";
				
				meta.spInsert = "proc_EmployeeOrganizationInsert";				
				meta.spUpdate = "proc_EmployeeOrganizationUpdate";		
				meta.spDelete = "proc_EmployeeOrganizationDelete";
				meta.spLoadAll = "proc_EmployeeOrganizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeOrganizationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeOrganizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
