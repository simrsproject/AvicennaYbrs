/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 9:55:49 PM
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
	abstract public class esTariffComponentCollection : esEntityCollectionWAuditLog
	{
		public esTariffComponentCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "TariffComponentCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esTariffComponentQuery query)
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
			this.InitQuery(query as esTariffComponentQuery);
		}
		#endregion
			
		virtual public TariffComponent DetachEntity(TariffComponent entity)
		{
			return base.DetachEntity(entity) as TariffComponent;
		}
		
		virtual public TariffComponent AttachEntity(TariffComponent entity)
		{
			return base.AttachEntity(entity) as TariffComponent;
		}
		
		virtual public void Combine(TariffComponentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TariffComponent this[int index]
		{
			get
			{
				return base[index] as TariffComponent;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TariffComponent);
		}
	}

	[Serializable]
	abstract public class esTariffComponent : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTariffComponentQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esTariffComponent()
		{
		}
	
		public esTariffComponent(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffComponentID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String tariffComponentID)
		{
			esTariffComponentQuery query = this.GetDynamicQuery();
			query.Where(query.TariffComponentID==tariffComponentID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffComponentID",tariffComponentID);
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
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "TariffComponentName": this.str.TariffComponentName = (string)value; break;
						case "SRTariffComponentType": this.str.SRTariffComponentType = (string)value; break;
						case "IsTariffParamedic": this.str.IsTariffParamedic = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsIncludeInTaxCalc": this.str.IsIncludeInTaxCalc = (string)value; break;
						case "SRPphType": this.str.SRPphType = (string)value; break;
						case "IsPrintParamedicInSlip": this.str.IsPrintParamedicInSlip = (string)value; break;
						case "IsAutoChecklistCorrectedFeeVerification": this.str.IsAutoChecklistCorrectedFeeVerification = (string)value; break;
						case "IsFeeVerificationDefaultSelected": this.str.IsFeeVerificationDefaultSelected = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsTariffParamedic":
						
							if (value == null || value is System.Boolean)
								this.IsTariffParamedic = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsIncludeInTaxCalc":
						
							if (value == null || value is System.Boolean)
								this.IsIncludeInTaxCalc = (System.Boolean?)value;
							break;
						case "IsPrintParamedicInSlip":
						
							if (value == null || value is System.Boolean)
								this.IsPrintParamedicInSlip = (System.Boolean?)value;
							break;
						case "IsAutoChecklistCorrectedFeeVerification":
						
							if (value == null || value is System.Boolean)
								this.IsAutoChecklistCorrectedFeeVerification = (System.Boolean?)value;
							break;
						case "IsFeeVerificationDefaultSelected":
						
							if (value == null || value is System.Boolean)
								this.IsFeeVerificationDefaultSelected = (System.Boolean?)value;
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
		/// Maps to TariffComponent.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.TariffComponentName
		/// </summary>
		virtual public System.String TariffComponentName
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.TariffComponentName);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.TariffComponentName, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.SRTariffComponentType
		/// </summary>
		virtual public System.String SRTariffComponentType
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.SRTariffComponentType);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.SRTariffComponentType, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.IsTariffParamedic
		/// </summary>
		virtual public System.Boolean? IsTariffParamedic
		{
			get
			{
				return base.GetSystemBoolean(TariffComponentMetadata.ColumnNames.IsTariffParamedic);
			}
			
			set
			{
				base.SetSystemBoolean(TariffComponentMetadata.ColumnNames.IsTariffParamedic, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TariffComponentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TariffComponentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.IsIncludeInTaxCalc
		/// </summary>
		virtual public System.Boolean? IsIncludeInTaxCalc
		{
			get
			{
				return base.GetSystemBoolean(TariffComponentMetadata.ColumnNames.IsIncludeInTaxCalc);
			}
			
			set
			{
				base.SetSystemBoolean(TariffComponentMetadata.ColumnNames.IsIncludeInTaxCalc, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.SRPphType
		/// </summary>
		virtual public System.String SRPphType
		{
			get
			{
				return base.GetSystemString(TariffComponentMetadata.ColumnNames.SRPphType);
			}
			
			set
			{
				base.SetSystemString(TariffComponentMetadata.ColumnNames.SRPphType, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.IsPrintParamedicInSlip
		/// </summary>
		virtual public System.Boolean? IsPrintParamedicInSlip
		{
			get
			{
				return base.GetSystemBoolean(TariffComponentMetadata.ColumnNames.IsPrintParamedicInSlip);
			}
			
			set
			{
				base.SetSystemBoolean(TariffComponentMetadata.ColumnNames.IsPrintParamedicInSlip, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.IsAutoChecklistCorrectedFeeVerification
		/// </summary>
		virtual public System.Boolean? IsAutoChecklistCorrectedFeeVerification
		{
			get
			{
				return base.GetSystemBoolean(TariffComponentMetadata.ColumnNames.IsAutoChecklistCorrectedFeeVerification);
			}
			
			set
			{
				base.SetSystemBoolean(TariffComponentMetadata.ColumnNames.IsAutoChecklistCorrectedFeeVerification, value);
			}
		}
		/// <summary>
		/// Maps to TariffComponent.IsFeeVerificationDefaultSelected
		/// </summary>
		virtual public System.Boolean? IsFeeVerificationDefaultSelected
		{
			get
			{
				return base.GetSystemBoolean(TariffComponentMetadata.ColumnNames.IsFeeVerificationDefaultSelected);
			}
			
			set
			{
				base.SetSystemBoolean(TariffComponentMetadata.ColumnNames.IsFeeVerificationDefaultSelected, value);
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
			public esStrings(esTariffComponent entity)
			{
				this.entity = entity;
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
			public System.String TariffComponentName
			{
				get
				{
					System.String data = entity.TariffComponentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentName = null;
					else entity.TariffComponentName = Convert.ToString(value);
				}
			}
			public System.String SRTariffComponentType
			{
				get
				{
					System.String data = entity.SRTariffComponentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffComponentType = null;
					else entity.SRTariffComponentType = Convert.ToString(value);
				}
			}
			public System.String IsTariffParamedic
			{
				get
				{
					System.Boolean? data = entity.IsTariffParamedic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTariffParamedic = null;
					else entity.IsTariffParamedic = Convert.ToBoolean(value);
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
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String IsIncludeInTaxCalc
			{
				get
				{
					System.Boolean? data = entity.IsIncludeInTaxCalc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeInTaxCalc = null;
					else entity.IsIncludeInTaxCalc = Convert.ToBoolean(value);
				}
			}
			public System.String SRPphType
			{
				get
				{
					System.String data = entity.SRPphType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPphType = null;
					else entity.SRPphType = Convert.ToString(value);
				}
			}
			public System.String IsPrintParamedicInSlip
			{
				get
				{
					System.Boolean? data = entity.IsPrintParamedicInSlip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrintParamedicInSlip = null;
					else entity.IsPrintParamedicInSlip = Convert.ToBoolean(value);
				}
			}
			public System.String IsAutoChecklistCorrectedFeeVerification
			{
				get
				{
					System.Boolean? data = entity.IsAutoChecklistCorrectedFeeVerification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoChecklistCorrectedFeeVerification = null;
					else entity.IsAutoChecklistCorrectedFeeVerification = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeeVerificationDefaultSelected
			{
				get
				{
					System.Boolean? data = entity.IsFeeVerificationDefaultSelected;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeVerificationDefaultSelected = null;
					else entity.IsFeeVerificationDefaultSelected = Convert.ToBoolean(value);
				}
			}
			private esTariffComponent entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTariffComponentQuery query)
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
				throw new Exception("esTariffComponent can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TariffComponent : esTariffComponent
	{	
	}

	[Serializable]
	abstract public class esTariffComponentQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return TariffComponentMetadata.Meta();
			}
		}	
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentName
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.TariffComponentName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRTariffComponentType
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.SRTariffComponentType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsTariffParamedic
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.IsTariffParamedic, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsIncludeInTaxCalc
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.IsIncludeInTaxCalc, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRPphType
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.SRPphType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPrintParamedicInSlip
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.IsPrintParamedicInSlip, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAutoChecklistCorrectedFeeVerification
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.IsAutoChecklistCorrectedFeeVerification, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsFeeVerificationDefaultSelected
		{
			get
			{
				return new esQueryItem(this, TariffComponentMetadata.ColumnNames.IsFeeVerificationDefaultSelected, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TariffComponentCollection")]
	public partial class TariffComponentCollection : esTariffComponentCollection, IEnumerable< TariffComponent>
	{
		public TariffComponentCollection()
		{

		}	
		
		public static implicit operator List< TariffComponent>(TariffComponentCollection coll)
		{
			List< TariffComponent> list = new List< TariffComponent>();
			
			foreach (TariffComponent emp in coll)
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
				return  TariffComponentMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TariffComponent(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TariffComponent();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public TariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TariffComponentQuery();
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
		public bool Load(TariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TariffComponent AddNew()
		{
			TariffComponent entity = base.AddNewEntity() as TariffComponent;
			
			return entity;		
		}
		public TariffComponent FindByPrimaryKey(String tariffComponentID)
		{
			return base.FindByPrimaryKey(tariffComponentID) as TariffComponent;
		}

		#region IEnumerable< TariffComponent> Members

		IEnumerator< TariffComponent> IEnumerable< TariffComponent>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TariffComponent;
			}
		}

		#endregion
		
		private TariffComponentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TariffComponent' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TariffComponent ({TariffComponentID})")]
	[Serializable]
	public partial class TariffComponent : esTariffComponent
	{
		public TariffComponent()
		{
		}	
	
		public TariffComponent(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TariffComponentMetadata.Meta();
			}
		}	
	
		override protected esTariffComponentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public TariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TariffComponentQuery();
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
		public bool Load(TariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private TariffComponentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TariffComponentQuery : esTariffComponentQuery
	{
		public TariffComponentQuery()
		{

		}		
		
		public TariffComponentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "TariffComponentQuery";
        }
	}

	[Serializable]
	public partial class TariffComponentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TariffComponentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.TariffComponentID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.TariffComponentName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.TariffComponentName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.SRTariffComponentType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.SRTariffComponentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.IsTariffParamedic, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TariffComponentMetadata.PropertyNames.IsTariffParamedic;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TariffComponentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.IsIncludeInTaxCalc, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TariffComponentMetadata.PropertyNames.IsIncludeInTaxCalc;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.SRPphType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TariffComponentMetadata.PropertyNames.SRPphType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.IsPrintParamedicInSlip, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TariffComponentMetadata.PropertyNames.IsPrintParamedicInSlip;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.IsAutoChecklistCorrectedFeeVerification, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TariffComponentMetadata.PropertyNames.IsAutoChecklistCorrectedFeeVerification;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(TariffComponentMetadata.ColumnNames.IsFeeVerificationDefaultSelected, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TariffComponentMetadata.PropertyNames.IsFeeVerificationDefaultSelected;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public TariffComponentMetadata Meta()
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
			public const string TariffComponentID = "TariffComponentID";
			public const string TariffComponentName = "TariffComponentName";
			public const string SRTariffComponentType = "SRTariffComponentType";
			public const string IsTariffParamedic = "IsTariffParamedic";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Notes = "Notes";
			public const string IsIncludeInTaxCalc = "IsIncludeInTaxCalc";
			public const string SRPphType = "SRPphType";
			public const string IsPrintParamedicInSlip = "IsPrintParamedicInSlip";
			public const string IsAutoChecklistCorrectedFeeVerification = "IsAutoChecklistCorrectedFeeVerification";
			public const string IsFeeVerificationDefaultSelected = "IsFeeVerificationDefaultSelected";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TariffComponentID = "TariffComponentID";
			public const string TariffComponentName = "TariffComponentName";
			public const string SRTariffComponentType = "SRTariffComponentType";
			public const string IsTariffParamedic = "IsTariffParamedic";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Notes = "Notes";
			public const string IsIncludeInTaxCalc = "IsIncludeInTaxCalc";
			public const string SRPphType = "SRPphType";
			public const string IsPrintParamedicInSlip = "IsPrintParamedicInSlip";
			public const string IsAutoChecklistCorrectedFeeVerification = "IsAutoChecklistCorrectedFeeVerification";
			public const string IsFeeVerificationDefaultSelected = "IsFeeVerificationDefaultSelected";
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
			lock (typeof(TariffComponentMetadata))
			{
				if(TariffComponentMetadata.mapDelegates == null)
				{
					TariffComponentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TariffComponentMetadata.meta == null)
				{
					TariffComponentMetadata.meta = new TariffComponentMetadata();
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
				
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTariffComponentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTariffParamedic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIncludeInTaxCalc", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPphType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrintParamedicInSlip", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAutoChecklistCorrectedFeeVerification", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeeVerificationDefaultSelected", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "TariffComponent";
				meta.Destination = "TariffComponent";
				meta.spInsert = "proc_TariffComponentInsert";				
				meta.spUpdate = "proc_TariffComponentUpdate";		
				meta.spDelete = "proc_TariffComponentDelete";
				meta.spLoadAll = "proc_TariffComponentLoadAll";
				meta.spLoadByPrimaryKey = "proc_TariffComponentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TariffComponentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
