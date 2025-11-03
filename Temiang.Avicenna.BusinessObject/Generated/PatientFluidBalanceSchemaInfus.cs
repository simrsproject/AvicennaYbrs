/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/4/2022 7:15:36 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esPatientFluidBalanceSchemaInfusCollection : esEntityCollectionWAuditLog
	{
		public esPatientFluidBalanceSchemaInfusCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientFluidBalanceSchemaInfusCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceSchemaInfusQuery query)
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
			this.InitQuery(query as esPatientFluidBalanceSchemaInfusQuery);
		}
		#endregion
			
		virtual public PatientFluidBalanceSchemaInfus DetachEntity(PatientFluidBalanceSchemaInfus entity)
		{
			return base.DetachEntity(entity) as PatientFluidBalanceSchemaInfus;
		}
		
		virtual public PatientFluidBalanceSchemaInfus AttachEntity(PatientFluidBalanceSchemaInfus entity)
		{
			return base.AttachEntity(entity) as PatientFluidBalanceSchemaInfus;
		}
		
		virtual public void Combine(PatientFluidBalanceSchemaInfusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientFluidBalanceSchemaInfus this[int index]
		{
			get
			{
				return base[index] as PatientFluidBalanceSchemaInfus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientFluidBalanceSchemaInfus);
		}
	}

	[Serializable]
	abstract public class esPatientFluidBalanceSchemaInfus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientFluidBalanceSchemaInfusQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientFluidBalanceSchemaInfus()
		{
		}
	
		public esPatientFluidBalanceSchemaInfus(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 sequenceNo, Int32 schemaInfusNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, schemaInfusNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, schemaInfusNo);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 sequenceNo, Int32 schemaInfusNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, schemaInfusNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, schemaInfusNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 sequenceNo, Int32 schemaInfusNo)
		{
			esPatientFluidBalanceSchemaInfusQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo==registrationNo, query.SequenceNo==sequenceNo, query.SchemaInfusNo==schemaInfusNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 sequenceNo, Int32 schemaInfusNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("SchemaInfusNo",schemaInfusNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SchemaInfusNo": this.str.SchemaInfusNo = (string)value; break;
						case "SchemaInfusName": this.str.SchemaInfusName = (string)value; break;
						case "QtyVolume": this.str.QtyVolume = (string)value; break;
						case "QtyPerHour": this.str.QtyPerHour = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SequenceNo":
						
							if (value == null || value is System.Int32)
								this.SequenceNo = (System.Int32?)value;
							break;
						case "SchemaInfusNo":
						
							if (value == null || value is System.Int32)
								this.SchemaInfusNo = (System.Int32?)value;
							break;
						case "QtyVolume":
						
							if (value == null || value is System.Decimal)
								this.QtyVolume = (System.Decimal?)value;
							break;
						case "QtyPerHour":
						
							if (value == null || value is System.Decimal)
								this.QtyPerHour = (System.Decimal?)value;
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
		/// Maps to PatientFluidBalanceSchemaInfus.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.SchemaInfusNo
		/// </summary>
		virtual public System.Int32? SchemaInfusNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.SchemaInfusName
		/// </summary>
		virtual public System.String SchemaInfusName
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusName);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusName, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.QtyVolume
		/// </summary>
		virtual public System.Decimal? QtyVolume
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyVolume);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyVolume, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.QtyPerHour
		/// </summary>
		virtual public System.Decimal? QtyPerHour
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyPerHour);
			}
			
			set
			{
				base.SetSystemDecimal(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyPerHour, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceSchemaInfus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties
		
		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
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
			public esStrings(esPatientFluidBalanceSchemaInfus entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.Int32? data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String SchemaInfusNo
			{
				get
				{
					System.Int32? data = entity.SchemaInfusNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchemaInfusNo = null;
					else entity.SchemaInfusNo = Convert.ToInt32(value);
				}
			}
			public System.String SchemaInfusName
			{
				get
				{
					System.String data = entity.SchemaInfusName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchemaInfusName = null;
					else entity.SchemaInfusName = Convert.ToString(value);
				}
			}
			public System.String QtyVolume
			{
				get
				{
					System.Decimal? data = entity.QtyVolume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyVolume = null;
					else entity.QtyVolume = Convert.ToDecimal(value);
				}
			}
			public System.String QtyPerHour
			{
				get
				{
					System.Decimal? data = entity.QtyPerHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyPerHour = null;
					else entity.QtyPerHour = Convert.ToDecimal(value);
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
			private esPatientFluidBalanceSchemaInfus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceSchemaInfusQuery query)
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
				throw new Exception("esPatientFluidBalanceSchemaInfus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientFluidBalanceSchemaInfus : esPatientFluidBalanceSchemaInfus
	{	
	}

	[Serializable]
	abstract public class esPatientFluidBalanceSchemaInfusQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceSchemaInfusMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SchemaInfusNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SchemaInfusName
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusName, esSystemType.String);
			}
		} 
			
		public esQueryItem QtyVolume
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyVolume, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem QtyPerHour
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyPerHour, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientFluidBalanceSchemaInfusCollection")]
	public partial class PatientFluidBalanceSchemaInfusCollection : esPatientFluidBalanceSchemaInfusCollection, IEnumerable< PatientFluidBalanceSchemaInfus>
	{
		public PatientFluidBalanceSchemaInfusCollection()
		{

		}	
		
		public static implicit operator List< PatientFluidBalanceSchemaInfus>(PatientFluidBalanceSchemaInfusCollection coll)
		{
			List< PatientFluidBalanceSchemaInfus> list = new List< PatientFluidBalanceSchemaInfus>();
			
			foreach (PatientFluidBalanceSchemaInfus emp in coll)
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
				return  PatientFluidBalanceSchemaInfusMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceSchemaInfusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientFluidBalanceSchemaInfus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientFluidBalanceSchemaInfus();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientFluidBalanceSchemaInfusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceSchemaInfusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PatientFluidBalanceSchemaInfusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientFluidBalanceSchemaInfus AddNew()
		{
			PatientFluidBalanceSchemaInfus entity = base.AddNewEntity() as PatientFluidBalanceSchemaInfus;
			
			return entity;		
		}
		public PatientFluidBalanceSchemaInfus FindByPrimaryKey(String registrationNo, Int32 sequenceNo, Int32 schemaInfusNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo, schemaInfusNo) as PatientFluidBalanceSchemaInfus;
		}

		#region IEnumerable< PatientFluidBalanceSchemaInfus> Members

		IEnumerator< PatientFluidBalanceSchemaInfus> IEnumerable< PatientFluidBalanceSchemaInfus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientFluidBalanceSchemaInfus;
			}
		}

		#endregion
		
		private PatientFluidBalanceSchemaInfusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientFluidBalanceSchemaInfus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientFluidBalanceSchemaInfus ({RegistrationNo, SequenceNo, SchemaInfusNo})")]
	[Serializable]
	public partial class PatientFluidBalanceSchemaInfus : esPatientFluidBalanceSchemaInfus
	{
		public PatientFluidBalanceSchemaInfus()
		{
		}	
	
		public PatientFluidBalanceSchemaInfus(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceSchemaInfusMetadata.Meta();
			}
		}	
	
		override protected esPatientFluidBalanceSchemaInfusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceSchemaInfusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientFluidBalanceSchemaInfusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceSchemaInfusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PatientFluidBalanceSchemaInfusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientFluidBalanceSchemaInfusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientFluidBalanceSchemaInfusQuery : esPatientFluidBalanceSchemaInfusQuery
	{
		public PatientFluidBalanceSchemaInfusQuery()
		{

		}		
		
		public PatientFluidBalanceSchemaInfusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientFluidBalanceSchemaInfusQuery";
        }
	}

	[Serializable]
	public partial class PatientFluidBalanceSchemaInfusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientFluidBalanceSchemaInfusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.SchemaInfusNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.SchemaInfusName;
			c.CharacterMaxLength = 250;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyVolume, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.QtyVolume;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyPerHour, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.QtyPerHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientFluidBalanceSchemaInfusMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceSchemaInfusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientFluidBalanceSchemaInfusMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string SchemaInfusNo = "SchemaInfusNo";
			public const string SchemaInfusName = "SchemaInfusName";
			public const string QtyVolume = "QtyVolume";
			public const string QtyPerHour = "QtyPerHour";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string SchemaInfusNo = "SchemaInfusNo";
			public const string SchemaInfusName = "SchemaInfusName";
			public const string QtyVolume = "QtyVolume";
			public const string QtyPerHour = "QtyPerHour";
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
			lock (typeof(PatientFluidBalanceSchemaInfusMetadata))
			{
				if(PatientFluidBalanceSchemaInfusMetadata.mapDelegates == null)
				{
					PatientFluidBalanceSchemaInfusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientFluidBalanceSchemaInfusMetadata.meta == null)
				{
					PatientFluidBalanceSchemaInfusMetadata.meta = new PatientFluidBalanceSchemaInfusMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SchemaInfusNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SchemaInfusName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyVolume", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyPerHour", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PatientFluidBalanceSchemaInfus";
				meta.Destination = "PatientFluidBalanceSchemaInfus";
				meta.spInsert = "proc_PatientFluidBalanceSchemaInfusInsert";				
				meta.spUpdate = "proc_PatientFluidBalanceSchemaInfusUpdate";		
				meta.spDelete = "proc_PatientFluidBalanceSchemaInfusDelete";
				meta.spLoadAll = "proc_PatientFluidBalanceSchemaInfusLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientFluidBalanceSchemaInfusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientFluidBalanceSchemaInfusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
