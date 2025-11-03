/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/18/2020 7:33:08 PM
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
	abstract public class esAppStandardReferenceItemCollection : esEntityCollectionWAuditLog
	{
		public esAppStandardReferenceItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppStandardReferenceItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppStandardReferenceItemQuery query)
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
			this.InitQuery(query as esAppStandardReferenceItemQuery);
		}
		#endregion

		virtual public AppStandardReferenceItem DetachEntity(AppStandardReferenceItem entity)
		{
			return base.DetachEntity(entity) as AppStandardReferenceItem;
		}

		virtual public AppStandardReferenceItem AttachEntity(AppStandardReferenceItem entity)
		{
			return base.AttachEntity(entity) as AppStandardReferenceItem;
		}

		virtual public void Combine(AppStandardReferenceItemCollection collection)
		{
			base.Combine(collection);
		}

		new public AppStandardReferenceItem this[int index]
		{
			get
			{
				return base[index] as AppStandardReferenceItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppStandardReferenceItem);
		}
	}

	[Serializable]
	abstract public class esAppStandardReferenceItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppStandardReferenceItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppStandardReferenceItem()
		{
		}

		public esAppStandardReferenceItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String standardReferenceID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardReferenceID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardReferenceID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String standardReferenceID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standardReferenceID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(standardReferenceID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String standardReferenceID, String itemID)
		{
			esAppStandardReferenceItemQuery query = this.GetDynamicQuery();
			query.Where(query.StandardReferenceID == standardReferenceID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String standardReferenceID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardReferenceID", standardReferenceID);
			parms.Add("ItemID", itemID);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ItemName": this.str.ItemName = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsUsedBySystem": this.str.IsUsedBySystem = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ReferenceID": this.str.ReferenceID = (string)value; break;
						case "coaID": this.str.coaID = (string)value; break;
						case "subledgerID": this.str.subledgerID = (string)value; break;
						case "CustomField": this.str.CustomField = (string)value; break;
						case "LineNumber": this.str.LineNumber = (string)value; break;
						case "NumericValue": this.str.NumericValue = (string)value; break;
						case "CustomField2": this.str.CustomField2 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
						case "coaID":

							if (value == null || value is System.Int32)
								this.coaID = (System.Int32?)value;
							break;
						case "subledgerID":

							if (value == null || value is System.Int32)
								this.subledgerID = (System.Int32?)value;
							break;
						case "LineNumber":

							if (value == null || value is System.Int32)
								this.LineNumber = (System.Int32?)value;
							break;
						case "NumericValue":

							if (value == null || value is System.Decimal)
								this.NumericValue = (System.Decimal?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to AppStandardReferenceItem.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ItemName);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ItemName, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.IsUsedBySystem
		/// </summary>
		virtual public System.Boolean? IsUsedBySystem
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceItemMetadata.ColumnNames.IsUsedBySystem);
			}

			set
			{
				base.SetSystemBoolean(AppStandardReferenceItemMetadata.ColumnNames.IsUsedBySystem, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AppStandardReferenceItemMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(AppStandardReferenceItemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.ReferenceID
		/// </summary>
		virtual public System.String ReferenceID
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ReferenceID);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.ReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.coaID
		/// </summary>
		virtual public System.Int32? coaID
		{
			get
			{
				return base.GetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.coaID);
			}

			set
			{
				base.SetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.coaID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.subledgerID
		/// </summary>
		virtual public System.Int32? subledgerID
		{
			get
			{
				return base.GetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.subledgerID);
			}

			set
			{
				base.SetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.subledgerID, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.CustomField
		/// </summary>
		virtual public System.String CustomField
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.CustomField);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.CustomField, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.LineNumber
		/// </summary>
		virtual public System.Int32? LineNumber
		{
			get
			{
				return base.GetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.LineNumber);
			}

			set
			{
				base.SetSystemInt32(AppStandardReferenceItemMetadata.ColumnNames.LineNumber, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.NumericValue
		/// </summary>
		virtual public System.Decimal? NumericValue
		{
			get
			{
				return base.GetSystemDecimal(AppStandardReferenceItemMetadata.ColumnNames.NumericValue);
			}

			set
			{
				base.SetSystemDecimal(AppStandardReferenceItemMetadata.ColumnNames.NumericValue, value);
			}
		}
		/// <summary>
		/// Maps to AppStandardReferenceItem.CustomField2
		/// </summary>
		virtual public System.String CustomField2
		{
			get
			{
				return base.GetSystemString(AppStandardReferenceItemMetadata.ColumnNames.CustomField2);
			}

			set
			{
				base.SetSystemString(AppStandardReferenceItemMetadata.ColumnNames.CustomField2, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esAppStandardReferenceItem entity)
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
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
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
			public System.String ReferenceID
			{
				get
				{
					System.String data = entity.ReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceID = null;
					else entity.ReferenceID = Convert.ToString(value);
				}
			}
			public System.String coaID
			{
				get
				{
					System.Int32? data = entity.coaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.coaID = null;
					else entity.coaID = Convert.ToInt32(value);
				}
			}
			public System.String subledgerID
			{
				get
				{
					System.Int32? data = entity.subledgerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.subledgerID = null;
					else entity.subledgerID = Convert.ToInt32(value);
				}
			}
			public System.String CustomField
			{
				get
				{
					System.String data = entity.CustomField;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomField = null;
					else entity.CustomField = Convert.ToString(value);
				}
			}
			public System.String LineNumber
			{
				get
				{
					System.Int32? data = entity.LineNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LineNumber = null;
					else entity.LineNumber = Convert.ToInt32(value);
				}
			}
			public System.String NumericValue
			{
				get
				{
					System.Decimal? data = entity.NumericValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumericValue = null;
					else entity.NumericValue = Convert.ToDecimal(value);
				}
			}
			public System.String CustomField2
			{
				get
				{
					System.String data = entity.CustomField2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomField2 = null;
					else entity.CustomField2 = Convert.ToString(value);
				}
			}
			private esAppStandardReferenceItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppStandardReferenceItemQuery query)
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
				throw new Exception("esAppStandardReferenceItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppStandardReferenceItem : esAppStandardReferenceItem
	{
	}

	[Serializable]
	abstract public class esAppStandardReferenceItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppStandardReferenceItemMetadata.Meta();
			}
		}

		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem IsUsedBySystem
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.IsUsedBySystem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.ReferenceID, esSystemType.String);
			}
		}

		public esQueryItem coaID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.coaID, esSystemType.Int32);
			}
		}

		public esQueryItem subledgerID
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.subledgerID, esSystemType.Int32);
			}
		}

		public esQueryItem CustomField
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.CustomField, esSystemType.String);
			}
		}

		public esQueryItem LineNumber
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.LineNumber, esSystemType.Int32);
			}
		}

		public esQueryItem NumericValue
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.NumericValue, esSystemType.Decimal);
			}
		}

		public esQueryItem CustomField2
		{
			get
			{
				return new esQueryItem(this, AppStandardReferenceItemMetadata.ColumnNames.CustomField2, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppStandardReferenceItemCollection")]
	public partial class AppStandardReferenceItemCollection : esAppStandardReferenceItemCollection, IEnumerable<AppStandardReferenceItem>
	{
		public AppStandardReferenceItemCollection()
		{

		}

		public static implicit operator List<AppStandardReferenceItem>(AppStandardReferenceItemCollection coll)
		{
			List<AppStandardReferenceItem> list = new List<AppStandardReferenceItem>();

			foreach (AppStandardReferenceItem emp in coll)
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
				return AppStandardReferenceItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppStandardReferenceItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppStandardReferenceItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppStandardReferenceItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppStandardReferenceItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppStandardReferenceItemQuery();
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
		public bool Load(AppStandardReferenceItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppStandardReferenceItem AddNew()
		{
			AppStandardReferenceItem entity = base.AddNewEntity() as AppStandardReferenceItem;

			return entity;
		}
		public AppStandardReferenceItem FindByPrimaryKey(String standardReferenceID, String itemID)
		{
			return base.FindByPrimaryKey(standardReferenceID, itemID) as AppStandardReferenceItem;
		}

		#region IEnumerable< AppStandardReferenceItem> Members

		IEnumerator<AppStandardReferenceItem> IEnumerable<AppStandardReferenceItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppStandardReferenceItem;
			}
		}

		#endregion

		private AppStandardReferenceItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppStandardReferenceItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppStandardReferenceItem ({StandardReferenceID, ItemID})")]
	[Serializable]
	public partial class AppStandardReferenceItem : esAppStandardReferenceItem
	{
		public AppStandardReferenceItem()
		{
		}

		public AppStandardReferenceItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppStandardReferenceItemMetadata.Meta();
			}
		}

		override protected esAppStandardReferenceItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppStandardReferenceItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppStandardReferenceItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppStandardReferenceItemQuery();
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
		public bool Load(AppStandardReferenceItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppStandardReferenceItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppStandardReferenceItemQuery : esAppStandardReferenceItemQuery
	{
		public AppStandardReferenceItemQuery()
		{

		}

		public AppStandardReferenceItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppStandardReferenceItemQuery";
		}
	}

	[Serializable]
	public partial class AppStandardReferenceItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppStandardReferenceItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.StandardReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.ItemName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.Note, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.IsUsedBySystem, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.IsUsedBySystem;
			c.HasDefault = true;
			c.Default = @"((1))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.ReferenceID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.ReferenceID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.coaID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.coaID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.subledgerID, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.subledgerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.CustomField, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.CustomField;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.LineNumber, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.LineNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.NumericValue, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.NumericValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppStandardReferenceItemMetadata.ColumnNames.CustomField2, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AppStandardReferenceItemMetadata.PropertyNames.CustomField2;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppStandardReferenceItemMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string StandardReferenceID = "StandardReferenceID";
			public const string ItemID = "ItemID";
			public const string ItemName = "ItemName";
			public const string Note = "Note";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceID = "ReferenceID";
			public const string coaID = "coaID";
			public const string subledgerID = "subledgerID";
			public const string CustomField = "CustomField";
			public const string LineNumber = "LineNumber";
			public const string NumericValue = "NumericValue";
			public const string CustomField2 = "CustomField2";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string StandardReferenceID = "StandardReferenceID";
			public const string ItemID = "ItemID";
			public const string ItemName = "ItemName";
			public const string Note = "Note";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceID = "ReferenceID";
			public const string coaID = "coaID";
			public const string subledgerID = "subledgerID";
			public const string CustomField = "CustomField";
			public const string LineNumber = "LineNumber";
			public const string NumericValue = "NumericValue";
			public const string CustomField2 = "CustomField2";
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
			lock (typeof(AppStandardReferenceItemMetadata))
			{
				if (AppStandardReferenceItemMetadata.mapDelegates == null)
				{
					AppStandardReferenceItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppStandardReferenceItemMetadata.meta == null)
				{
					AppStandardReferenceItemMetadata.meta = new AppStandardReferenceItemMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("coaID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("subledgerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CustomField", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LineNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumericValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CustomField2", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppStandardReferenceItem";
				meta.Destination = "AppStandardReferenceItem";
				meta.spInsert = "proc_AppStandardReferenceItemInsert";
				meta.spUpdate = "proc_AppStandardReferenceItemUpdate";
				meta.spDelete = "proc_AppStandardReferenceItemDelete";
				meta.spLoadAll = "proc_AppStandardReferenceItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppStandardReferenceItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppStandardReferenceItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
