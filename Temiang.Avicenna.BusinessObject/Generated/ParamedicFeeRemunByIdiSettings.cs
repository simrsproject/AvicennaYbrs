/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/25/2021 9:53:43 AM
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
	abstract public class esParamedicFeeRemunByIdiSettingsCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeRemunByIdiSettingsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeRemunByIdiSettingsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiSettingsQuery query)
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
			this.InitQuery(query as esParamedicFeeRemunByIdiSettingsQuery);
		}
		#endregion
			
		virtual public ParamedicFeeRemunByIdiSettings DetachEntity(ParamedicFeeRemunByIdiSettings entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeRemunByIdiSettings;
		}
		
		virtual public ParamedicFeeRemunByIdiSettings AttachEntity(ParamedicFeeRemunByIdiSettings entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeRemunByIdiSettings;
		}
		
		virtual public void Combine(ParamedicFeeRemunByIdiSettingsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeRemunByIdiSettings this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeRemunByIdiSettings;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeRemunByIdiSettings);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiSettings : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeRemunByIdiSettingsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeRemunByIdiSettings()
		{
		}
	
		public esParamedicFeeRemunByIdiSettings(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 settingID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(settingID);
			else
				return LoadByPrimaryKeyStoredProcedure(settingID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 settingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(settingID);
			else
				return LoadByPrimaryKeyStoredProcedure(settingID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 settingID)
		{
			esParamedicFeeRemunByIdiSettingsQuery query = this.GetDynamicQuery();
			query.Where(query.SettingID==settingID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 settingID)
		{
			esParameters parms = new esParameters();
			parms.Add("SettingID",settingID);
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
						case "SettingID": this.str.SettingID = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "MultiplierValue": this.str.MultiplierValue = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SettingID":
						
							if (value == null || value is System.Int32)
								this.SettingID = (System.Int32?)value;
							break;
						case "MultiplierValue":
						
							if (value == null || value is System.Decimal)
								this.MultiplierValue = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeRemunByIdiSettings.SettingID
		/// </summary>
		virtual public System.Int32? SettingID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SettingID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SettingID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.MultiplierValue
		/// </summary>
		virtual public System.Decimal? MultiplierValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.MultiplierValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.MultiplierValue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiSettings.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeRemunByIdiSettings entity)
			{
				this.entity = entity;
			}
			public System.String SettingID
			{
				get
				{
					System.Int32? data = entity.SettingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SettingID = null;
					else entity.SettingID = Convert.ToInt32(value);
				}
			}
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String MultiplierValue
			{
				get
				{
					System.Decimal? data = entity.MultiplierValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MultiplierValue = null;
					else entity.MultiplierValue = Convert.ToDecimal(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			private esParamedicFeeRemunByIdiSettings entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiSettingsQuery query)
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
				throw new Exception("esParamedicFeeRemunByIdiSettings can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeRemunByIdiSettings : esParamedicFeeRemunByIdiSettings
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiSettingsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiSettingsMetadata.Meta();
			}
		}	
			
		public esQueryItem SettingID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SettingID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem MultiplierValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.MultiplierValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeRemunByIdiSettingsCollection")]
	public partial class ParamedicFeeRemunByIdiSettingsCollection : esParamedicFeeRemunByIdiSettingsCollection, IEnumerable< ParamedicFeeRemunByIdiSettings>
	{
		public ParamedicFeeRemunByIdiSettingsCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeRemunByIdiSettings>(ParamedicFeeRemunByIdiSettingsCollection coll)
		{
			List< ParamedicFeeRemunByIdiSettings> list = new List< ParamedicFeeRemunByIdiSettings>();
			
			foreach (ParamedicFeeRemunByIdiSettings emp in coll)
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
				return  ParamedicFeeRemunByIdiSettingsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiSettingsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeRemunByIdiSettings(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeRemunByIdiSettings();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiSettingsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiSettingsQuery();
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
		public bool Load(ParamedicFeeRemunByIdiSettingsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeRemunByIdiSettings AddNew()
		{
			ParamedicFeeRemunByIdiSettings entity = base.AddNewEntity() as ParamedicFeeRemunByIdiSettings;
			
			return entity;		
		}
		public ParamedicFeeRemunByIdiSettings FindByPrimaryKey(Int32 settingID)
		{
			return base.FindByPrimaryKey(settingID) as ParamedicFeeRemunByIdiSettings;
		}

		#region IEnumerable< ParamedicFeeRemunByIdiSettings> Members

		IEnumerator< ParamedicFeeRemunByIdiSettings> IEnumerable< ParamedicFeeRemunByIdiSettings>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeRemunByIdiSettings;
			}
		}

		#endregion
		
		private ParamedicFeeRemunByIdiSettingsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeRemunByIdiSettings' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeRemunByIdiSettings ({SettingID})")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiSettings : esParamedicFeeRemunByIdiSettings
	{
		public ParamedicFeeRemunByIdiSettings()
		{
		}	
	
		public ParamedicFeeRemunByIdiSettings(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiSettingsMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeRemunByIdiSettingsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiSettingsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiSettingsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiSettingsQuery();
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
		public bool Load(ParamedicFeeRemunByIdiSettingsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeRemunByIdiSettingsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiSettingsQuery : esParamedicFeeRemunByIdiSettingsQuery
	{
		public ParamedicFeeRemunByIdiSettingsQuery()
		{

		}		
		
		public ParamedicFeeRemunByIdiSettingsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeRemunByIdiSettingsQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeRemunByIdiSettingsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeRemunByIdiSettingsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SettingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.SettingID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.SmfID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemGroupID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.MultiplierValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.MultiplierValue;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiSettingsMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiSettingsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeRemunByIdiSettingsMetadata Meta()
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
			public const string SettingID = "SettingID";
			public const string SmfID = "SmfID";
			public const string ParamedicID = "ParamedicID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string MultiplierValue = "MultiplierValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SettingID = "SettingID";
			public const string SmfID = "SmfID";
			public const string ParamedicID = "ParamedicID";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemID = "ItemID";
			public const string MultiplierValue = "MultiplierValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(ParamedicFeeRemunByIdiSettingsMetadata))
			{
				if(ParamedicFeeRemunByIdiSettingsMetadata.mapDelegates == null)
				{
					ParamedicFeeRemunByIdiSettingsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeRemunByIdiSettingsMetadata.meta == null)
				{
					ParamedicFeeRemunByIdiSettingsMetadata.meta = new ParamedicFeeRemunByIdiSettingsMetadata();
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
				
				meta.AddTypeMap("SettingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MultiplierValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ParamedicFeeRemunByIdiSettings";
				meta.Destination = "ParamedicFeeRemunByIdiSettings";
				meta.spInsert = "proc_ParamedicFeeRemunByIdiSettingsInsert";				
				meta.spUpdate = "proc_ParamedicFeeRemunByIdiSettingsUpdate";		
				meta.spDelete = "proc_ParamedicFeeRemunByIdiSettingsDelete";
				meta.spLoadAll = "proc_ParamedicFeeRemunByIdiSettingsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeRemunByIdiSettingsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeRemunByIdiSettingsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
