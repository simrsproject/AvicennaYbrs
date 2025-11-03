/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/18/2017 12:32:03 PM
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
	abstract public class esAppStandardReferenceCollection : esEntityCollectionWAuditLog
	{
		public esAppStandardReferenceCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppStandardReferenceCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppStandardReferenceQuery query)
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
			this.InitQuery(query as esAppStandardReferenceQuery);
		}
		#endregion
			
		virtual public AppStandardReference DetachEntity(AppStandardReference entity)
		{
			return base.DetachEntity(entity) as AppStandardReference;
		}
		
		virtual public AppStandardReference AttachEntity(AppStandardReference entity)
		{
			return base.AttachEntity(entity) as AppStandardReference;
		}
		
		virtual public void Combine(AppStandardReferenceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppStandardReference this[int index]
		{
			get
			{
				return base[index] as AppStandardReference;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppStandardReference);
		}
	}

	[Serializable]
	abstract public class esAppStandardReference : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppStandardReferenceQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppStandardReference()
		{
		}
	
		public esAppStandardReference(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String standardReferenceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardReferenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardReferenceID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String standardReferenceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardReferenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardReferenceID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String standardReferenceID)
		{
			esAppStandardReferenceQuery query = this.GetDynamicQuery();
			query.Where(query.StandardReferenceID==standardReferenceID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String standardReferenceID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardReferenceID",standardReferenceID);
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
						case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;
						case "StandardReferenceName": this.str.StandardReferenceName = (string)value; break;
						case "ItemLength": this.str.ItemLength = (string)value; break;
						case "IsUsedBySystem": this.str.IsUsedBySystem = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "StandardReferenceGroup": this.str.StandardReferenceGroup = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "HasCOA": this.str.HasCOA = (string)value; break;
						case "IsNumericValue": this.str.IsNumericValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ItemLength":
						
							if (value == null || value is System.Int32)
								this.ItemLength = (System.Int32?)value;
							break;
						case "IsUsedBySystem":
						
							if (value == null || value is System.Boolean)
								this.IsUsedBySystem = (System.Boolean?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "HasCOA":
						
							if (value == null || value is System.Boolean)
								this.HasCOA = (System.Boolean?)value;
							break;
						case "IsNumericValue":
						
							if (value == null || value is System.Boolean)
								this.IsNumericValue = (System.Boolean?)value;
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
		/// Maps to AppStandardReference.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				base.SetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.StandardReferenceName
		/// </summary>
		virtual public System.String StandardReferenceName
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceName);
			}
			
			set
			{
				base.SetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceName, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.ItemLength
		/// </summary>
		virtual public System.Int32? ItemLength
		{
			get
			{
				return base.GetSystemInt32(AppStandardReferenceMetadata.ColumnNames.ItemLength);
			}
			
			set
			{
				base.SetSystemInt32(AppStandardReferenceMetadata.ColumnNames.ItemLength, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.IsUsedBySystem
		/// </summary>
		virtual public System.Boolean? IsUsedBySystem
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				base.SetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsUsedBySystem, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.StandardReferenceGroup
		/// </summary>
		virtual public System.String StandardReferenceGroup
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceGroup);
			}
			
			set
			{
				base.SetSystemString(AppStandardReferenceMetadata.ColumnNames.StandardReferenceGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(AppStandardReferenceMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppStandardReferenceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppStandardReferenceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppStandardReferenceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.HasCOA
		/// </summary>
		virtual public System.Boolean? HasCOA
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.HasCOA);
			}
			
			set
			{
				base.SetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.HasCOA, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReference.IsNumericValue
		/// </summary>
		virtual public System.Boolean? IsNumericValue
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsNumericValue);
			}
			
			set
			{
				base.SetSystemBoolean(AppStandardReferenceMetadata.ColumnNames.IsNumericValue, value);
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
			public esStrings(esAppStandardReference entity)
			{
				this.entity = entity;
			}
			public System.String StandardReferenceID
			{
				get
				{
					System.String data = entity.StandardReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardReferenceID = null;
					else entity.StandardReferenceID = Convert.ToString(value);
				}
			}
			public System.String StandardReferenceName
			{
				get
				{
					System.String data = entity.StandardReferenceName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardReferenceName = null;
					else entity.StandardReferenceName = Convert.ToString(value);
				}
			}
			public System.String ItemLength
			{
				get
				{
					System.Int32? data = entity.ItemLength;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemLength = null;
					else entity.ItemLength = Convert.ToInt32(value);
				}
			}
			public System.String IsUsedBySystem
			{
				get
				{
					System.Boolean? data = entity.IsUsedBySystem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsedBySystem = null;
					else entity.IsUsedBySystem = Convert.ToBoolean(value);
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
			public System.String StandardReferenceGroup
			{
				get
				{
					System.String data = entity.StandardReferenceGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardReferenceGroup = null;
					else entity.StandardReferenceGroup = Convert.ToString(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			public System.String HasCOA
			{
				get
				{
					System.Boolean? data = entity.HasCOA;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HasCOA = null;
					else entity.HasCOA = Convert.ToBoolean(value);
				}
			}
			public System.String IsNumericValue
			{
				get
				{
					System.Boolean? data = entity.IsNumericValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNumericValue = null;
					else entity.IsNumericValue = Convert.ToBoolean(value);
				}
			}
			private esAppStandardReference entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppStandardReferenceQuery query)
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
				throw new Exception("esAppStandardReference can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppStandardReference : esAppStandardReference
	{	
	}

	[Serializable]
	abstract public class esAppStandardReferenceQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppStandardReferenceMetadata.Meta();
			}
		}	
			
		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		} 
			
		public esQueryItem StandardReferenceName
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.StandardReferenceName, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemLength
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.ItemLength, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsUsedBySystem
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.IsUsedBySystem, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem StandardReferenceGroup
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.StandardReferenceGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem HasCOA
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.HasCOA, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsNumericValue
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceMetadata.ColumnNames.IsNumericValue, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppStandardReferenceCollection")]
	public partial class AppStandardReferenceCollection : esAppStandardReferenceCollection, IEnumerable< AppStandardReference>
	{
		public AppStandardReferenceCollection()
		{

		}	
		
		public static implicit operator List< AppStandardReference>(AppStandardReferenceCollection coll)
		{
			List< AppStandardReference> list = new List< AppStandardReference>();
			
			foreach (AppStandardReference emp in coll)
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
				return  AppStandardReferenceMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppStandardReferenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppStandardReference(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppStandardReference();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppStandardReferenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppStandardReferenceQuery();
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
		public bool Load(AppStandardReferenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppStandardReference AddNew()
		{
			AppStandardReference entity = base.AddNewEntity() as AppStandardReference;
			
			return entity;		
		}
		public AppStandardReference FindByPrimaryKey(String standardReferenceID)
		{
			return base.FindByPrimaryKey(standardReferenceID) as AppStandardReference;
		}

		#region IEnumerable< AppStandardReference> Members

		IEnumerator< AppStandardReference> IEnumerable< AppStandardReference>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppStandardReference;
			}
		}

		#endregion
		
		private AppStandardReferenceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppStandardReference' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppStandardReference ({StandardReferenceID})")]
	[Serializable]
	public partial class AppStandardReference : esAppStandardReference
	{
		public AppStandardReference()
		{
		}	
	
		public AppStandardReference(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppStandardReferenceMetadata.Meta();
			}
		}	
	
		override protected esAppStandardReferenceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppStandardReferenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppStandardReferenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppStandardReferenceQuery();
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
		public bool Load(AppStandardReferenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppStandardReferenceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppStandardReferenceQuery : esAppStandardReferenceQuery
	{
		public AppStandardReferenceQuery()
		{

		}		
		
		public AppStandardReferenceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppStandardReferenceQuery";
        }
	}

	[Serializable]
	public partial class AppStandardReferenceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppStandardReferenceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.StandardReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.StandardReferenceName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.StandardReferenceName;
			c.CharacterMaxLength = 200;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.ItemLength, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.ItemLength;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.IsUsedBySystem, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.IsUsedBySystem;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.StandardReferenceGroup, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.StandardReferenceGroup;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.Note, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.HasCOA, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.HasCOA;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppStandardReferenceMetadata.ColumnNames.IsNumericValue, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceMetadata.PropertyNames.IsNumericValue;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppStandardReferenceMetadata Meta()
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
			public const string StandardReferenceID = "StandardReferenceID";
			public const string StandardReferenceName = "StandardReferenceName";
			public const string ItemLength = "ItemLength";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string IsActive = "IsActive";
			public const string StandardReferenceGroup = "StandardReferenceGroup";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string HasCOA = "HasCOA";
			public const string IsNumericValue = "IsNumericValue";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string StandardReferenceID = "StandardReferenceID";
			public const string StandardReferenceName = "StandardReferenceName";
			public const string ItemLength = "ItemLength";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string IsActive = "IsActive";
			public const string StandardReferenceGroup = "StandardReferenceGroup";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string HasCOA = "HasCOA";
			public const string IsNumericValue = "IsNumericValue";
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
			lock (typeof(AppStandardReferenceMetadata))
			{
				if(AppStandardReferenceMetadata.mapDelegates == null)
				{
					AppStandardReferenceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppStandardReferenceMetadata.meta == null)
				{
					AppStandardReferenceMetadata.meta = new AppStandardReferenceMetadata();
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
				
				meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StandardReferenceName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemLength", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("StandardReferenceGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HasCOA", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNumericValue", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "AppStandardReference";
				meta.Destination = "AppStandardReference";
				meta.spInsert = "proc_AppStandardReferenceInsert";				
				meta.spUpdate = "proc_AppStandardReferenceUpdate";		
				meta.spDelete = "proc_AppStandardReferenceDelete";
				meta.spLoadAll = "proc_AppStandardReferenceLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppStandardReferenceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppStandardReferenceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
