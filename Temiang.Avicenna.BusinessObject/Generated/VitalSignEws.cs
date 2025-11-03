/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/28/2023 7:49:03 AM
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
	abstract public class esVitalSignEwsCollection : esEntityCollectionWAuditLog
	{
		public esVitalSignEwsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VitalSignEwsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVitalSignEwsQuery query)
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
			this.InitQuery(query as esVitalSignEwsQuery);
		}
		#endregion
			
		virtual public VitalSignEws DetachEntity(VitalSignEws entity)
		{
			return base.DetachEntity(entity) as VitalSignEws;
		}
		
		virtual public VitalSignEws AttachEntity(VitalSignEws entity)
		{
			return base.AttachEntity(entity) as VitalSignEws;
		}
		
		virtual public void Combine(VitalSignEwsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VitalSignEws this[int index]
		{
			get
			{
				return base[index] as VitalSignEws;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VitalSignEws);
		}
	}

	[Serializable]
	abstract public class esVitalSignEws : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVitalSignEwsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVitalSignEws()
		{
		}
	
		public esVitalSignEws(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sREwsType, String vitalSignID, Int32 startAgeInDay)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sREwsType, vitalSignID, startAgeInDay);
			else
				return LoadByPrimaryKeyStoredProcedure(sREwsType, vitalSignID, startAgeInDay);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sREwsType, String vitalSignID, Int32 startAgeInDay)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sREwsType, vitalSignID, startAgeInDay);
			else
				return LoadByPrimaryKeyStoredProcedure(sREwsType, vitalSignID, startAgeInDay);
		}
	
		private bool LoadByPrimaryKeyDynamic(String sREwsType, String vitalSignID, Int32 startAgeInDay)
		{
			esVitalSignEwsQuery query = this.GetDynamicQuery();
			query.Where(query.SREwsType == sREwsType, query.VitalSignID == vitalSignID, query.StartAgeInDay == startAgeInDay);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String sREwsType, String vitalSignID, Int32 startAgeInDay)
		{
			esParameters parms = new esParameters();
			parms.Add("SREwsType",sREwsType);
			parms.Add("VitalSignID",vitalSignID);
			parms.Add("StartAgeInDay",startAgeInDay);
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
						case "SREwsType": this.str.SREwsType = (string)value; break;
						case "VitalSignID": this.str.VitalSignID = (string)value; break;
						case "StartAgeInDay": this.str.StartAgeInDay = (string)value; break;
						case "EndAgeInDay": this.str.EndAgeInDay = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
						case "ChartMinValue": this.str.ChartMinValue = (string)value; break;
						case "ChartMaxValue": this.str.ChartMaxValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsExcludeFromScoreEws": this.str.IsExcludeFromScoreEws = (string)value; break;
						case "ChartYAxisStep": this.str.ChartYAxisStep = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartAgeInDay":
						
							if (value == null || value is System.Int32)
								this.StartAgeInDay = (System.Int32?)value;
							break;
						case "EndAgeInDay":
						
							if (value == null || value is System.Int32)
								this.EndAgeInDay = (System.Int32?)value;
							break;
						case "IndexNo":
						
							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "ChartMinValue":
						
							if (value == null || value is System.Decimal)
								this.ChartMinValue = (System.Decimal?)value;
							break;
						case "ChartMaxValue":
						
							if (value == null || value is System.Decimal)
								this.ChartMaxValue = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsExcludeFromScoreEws":
						
							if (value == null || value is System.Boolean)
								this.IsExcludeFromScoreEws = (System.Boolean?)value;
							break;
						case "ChartYAxisStep":
						
							if (value == null || value is System.Decimal)
								this.ChartYAxisStep = (System.Decimal?)value;
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
		/// Maps to VitalSignEws.SREwsType
		/// </summary>
		virtual public System.String SREwsType
		{
			get
			{
				return base.GetSystemString(VitalSignEwsMetadata.ColumnNames.SREwsType);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsMetadata.ColumnNames.SREwsType, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(VitalSignEwsMetadata.ColumnNames.VitalSignID);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsMetadata.ColumnNames.VitalSignID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.StartAgeInDay
		/// </summary>
		virtual public System.Int32? StartAgeInDay
		{
			get
			{
				return base.GetSystemInt32(VitalSignEwsMetadata.ColumnNames.StartAgeInDay);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignEwsMetadata.ColumnNames.StartAgeInDay, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.EndAgeInDay
		/// </summary>
		virtual public System.Int32? EndAgeInDay
		{
			get
			{
				return base.GetSystemInt32(VitalSignEwsMetadata.ColumnNames.EndAgeInDay);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignEwsMetadata.ColumnNames.EndAgeInDay, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(VitalSignEwsMetadata.ColumnNames.IndexNo);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignEwsMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.ChartMinValue
		/// </summary>
		virtual public System.Decimal? ChartMinValue
		{
			get
			{
				return base.GetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartMinValue);
			}
			
			set
			{
				base.SetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartMinValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.ChartMaxValue
		/// </summary>
		virtual public System.Decimal? ChartMaxValue
		{
			get
			{
				return base.GetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartMaxValue);
			}
			
			set
			{
				base.SetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartMaxValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VitalSignEwsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VitalSignEwsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VitalSignEwsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.IsExcludeFromScoreEws
		/// </summary>
		virtual public System.Boolean? IsExcludeFromScoreEws
		{
			get
			{
				return base.GetSystemBoolean(VitalSignEwsMetadata.ColumnNames.IsExcludeFromScoreEws);
			}
			
			set
			{
				base.SetSystemBoolean(VitalSignEwsMetadata.ColumnNames.IsExcludeFromScoreEws, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEws.ChartYAxisStep
		/// </summary>
		virtual public System.Decimal? ChartYAxisStep
		{
			get
			{
				return base.GetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartYAxisStep);
			}
			
			set
			{
				base.SetSystemDecimal(VitalSignEwsMetadata.ColumnNames.ChartYAxisStep, value);
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
			public esStrings(esVitalSignEws entity)
			{
				this.entity = entity;
			}
			public System.String SREwsType
			{
				get
				{
					System.String data = entity.SREwsType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREwsType = null;
					else entity.SREwsType = Convert.ToString(value);
				}
			}
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
				}
			}
			public System.String StartAgeInDay
			{
				get
				{
					System.Int32? data = entity.StartAgeInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartAgeInDay = null;
					else entity.StartAgeInDay = Convert.ToInt32(value);
				}
			}
			public System.String EndAgeInDay
			{
				get
				{
					System.Int32? data = entity.EndAgeInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndAgeInDay = null;
					else entity.EndAgeInDay = Convert.ToInt32(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			public System.String ChartMinValue
			{
				get
				{
					System.Decimal? data = entity.ChartMinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartMinValue = null;
					else entity.ChartMinValue = Convert.ToDecimal(value);
				}
			}
			public System.String ChartMaxValue
			{
				get
				{
					System.Decimal? data = entity.ChartMaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartMaxValue = null;
					else entity.ChartMaxValue = Convert.ToDecimal(value);
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
			public System.String IsExcludeFromScoreEws
			{
				get
				{
					System.Boolean? data = entity.IsExcludeFromScoreEws;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExcludeFromScoreEws = null;
					else entity.IsExcludeFromScoreEws = Convert.ToBoolean(value);
				}
			}
			public System.String ChartYAxisStep
			{
				get
				{
					System.Decimal? data = entity.ChartYAxisStep;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartYAxisStep = null;
					else entity.ChartYAxisStep = Convert.ToDecimal(value);
				}
			}
			private esVitalSignEws entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVitalSignEwsQuery query)
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
				throw new Exception("esVitalSignEws can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VitalSignEws : esVitalSignEws
	{	
	}

	[Serializable]
	abstract public class esVitalSignEwsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignEwsMetadata.Meta();
			}
		}	
			
		public esQueryItem SREwsType
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.SREwsType, esSystemType.String);
			}
		} 
			
		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		} 
			
		public esQueryItem StartAgeInDay
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.StartAgeInDay, esSystemType.Int32);
			}
		} 
			
		public esQueryItem EndAgeInDay
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.EndAgeInDay, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartMinValue
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.ChartMinValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ChartMaxValue
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.ChartMaxValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsExcludeFromScoreEws
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.IsExcludeFromScoreEws, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartYAxisStep
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsMetadata.ColumnNames.ChartYAxisStep, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VitalSignEwsCollection")]
	public partial class VitalSignEwsCollection : esVitalSignEwsCollection, IEnumerable< VitalSignEws>
	{
		public VitalSignEwsCollection()
		{

		}	
		
		public static implicit operator List< VitalSignEws>(VitalSignEwsCollection coll)
		{
			List< VitalSignEws> list = new List< VitalSignEws>();
			
			foreach (VitalSignEws emp in coll)
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
				return  VitalSignEwsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignEwsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VitalSignEws(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VitalSignEws();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VitalSignEwsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignEwsQuery();
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
		public bool Load(VitalSignEwsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VitalSignEws AddNew()
		{
			VitalSignEws entity = base.AddNewEntity() as VitalSignEws;
			
			return entity;		
		}
		public VitalSignEws FindByPrimaryKey(String sREwsType, String vitalSignID, Int32 startAgeInDay)
		{
			return base.FindByPrimaryKey(sREwsType, vitalSignID, startAgeInDay) as VitalSignEws;
		}

		#region IEnumerable< VitalSignEws> Members

		IEnumerator< VitalSignEws> IEnumerable< VitalSignEws>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VitalSignEws;
			}
		}

		#endregion
		
		private VitalSignEwsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VitalSignEws' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VitalSignEws ({SREwsType, VitalSignID, StartAgeInDay})")]
	[Serializable]
	public partial class VitalSignEws : esVitalSignEws
	{
		public VitalSignEws()
		{
		}	
	
		public VitalSignEws(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignEwsMetadata.Meta();
			}
		}	
	
		override protected esVitalSignEwsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignEwsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VitalSignEwsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignEwsQuery();
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
		public bool Load(VitalSignEwsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VitalSignEwsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VitalSignEwsQuery : esVitalSignEwsQuery
	{
		public VitalSignEwsQuery()
		{

		}		
		
		public VitalSignEwsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VitalSignEwsQuery";
        }
	}

	[Serializable]
	public partial class VitalSignEwsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VitalSignEwsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.SREwsType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.SREwsType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('EWS')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.VitalSignID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.VitalSignID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.StartAgeInDay, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.StartAgeInDay;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.EndAgeInDay, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.EndAgeInDay;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((100000))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.IndexNo, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.ChartMinValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.ChartMinValue;
			c.NumericPrecision = 4;
			c.NumericScale = 1;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.ChartMaxValue, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.ChartMaxValue;
			c.NumericPrecision = 4;
			c.NumericScale = 1;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.IsExcludeFromScoreEws, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.IsExcludeFromScoreEws;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsMetadata.ColumnNames.ChartYAxisStep, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VitalSignEwsMetadata.PropertyNames.ChartYAxisStep;
			c.NumericPrecision = 4;
			c.NumericScale = 1;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VitalSignEwsMetadata Meta()
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
			public const string SREwsType = "SREwsType";
			public const string VitalSignID = "VitalSignID";
			public const string StartAgeInDay = "StartAgeInDay";
			public const string EndAgeInDay = "EndAgeInDay";
			public const string IndexNo = "IndexNo";
			public const string ChartMinValue = "ChartMinValue";
			public const string ChartMaxValue = "ChartMaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsExcludeFromScoreEws = "IsExcludeFromScoreEws";
			public const string ChartYAxisStep = "ChartYAxisStep";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SREwsType = "SREwsType";
			public const string VitalSignID = "VitalSignID";
			public const string StartAgeInDay = "StartAgeInDay";
			public const string EndAgeInDay = "EndAgeInDay";
			public const string IndexNo = "IndexNo";
			public const string ChartMinValue = "ChartMinValue";
			public const string ChartMaxValue = "ChartMaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsExcludeFromScoreEws = "IsExcludeFromScoreEws";
			public const string ChartYAxisStep = "ChartYAxisStep";
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
			lock (typeof(VitalSignEwsMetadata))
			{
				if(VitalSignEwsMetadata.mapDelegates == null)
				{
					VitalSignEwsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VitalSignEwsMetadata.meta == null)
				{
					VitalSignEwsMetadata.meta = new VitalSignEwsMetadata();
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
				
				meta.AddTypeMap("SREwsType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartAgeInDay", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EndAgeInDay", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartMinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ChartMaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsExcludeFromScoreEws", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartYAxisStep", new esTypeMap("numeric", "System.Decimal"));
		

				meta.Source = "VitalSignEws";
				meta.Destination = "VitalSignEws";
				meta.spInsert = "proc_VitalSignEwsInsert";				
				meta.spUpdate = "proc_VitalSignEwsUpdate";		
				meta.spDelete = "proc_VitalSignEwsDelete";
				meta.spLoadAll = "proc_VitalSignEwsLoadAll";
				meta.spLoadByPrimaryKey = "proc_VitalSignEwsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VitalSignEwsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
