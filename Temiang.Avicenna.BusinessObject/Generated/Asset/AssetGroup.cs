/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2022 11:34:28 AM
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
	abstract public class esAssetGroupCollection : esEntityCollectionWAuditLog
	{
		public esAssetGroupCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetGroupCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetGroupQuery query)
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
			this.InitQuery(query as esAssetGroupQuery);
		}
		#endregion

		virtual public AssetGroup DetachEntity(AssetGroup entity)
		{
			return base.DetachEntity(entity) as AssetGroup;
		}

		virtual public AssetGroup AttachEntity(AssetGroup entity)
		{
			return base.AttachEntity(entity) as AssetGroup;
		}

		virtual public void Combine(AssetGroupCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetGroup this[int index]
		{
			get
			{
				return base[index] as AssetGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetGroup);
		}
	}

	[Serializable]
	abstract public class esAssetGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetGroupQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetGroup()
		{
		}

		public esAssetGroup(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String assetGroupId)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetGroupId);
			else
				return LoadByPrimaryKeyStoredProcedure(assetGroupId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String assetGroupId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetGroupId);
			else
				return LoadByPrimaryKeyStoredProcedure(assetGroupId);
		}

		private bool LoadByPrimaryKeyDynamic(String assetGroupId)
		{
			esAssetGroupQuery query = this.GetDynamicQuery();
			query.Where(query.AssetGroupId == assetGroupId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String assetGroupId)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetGroupId", assetGroupId);
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
						case "AssetGroupId": this.str.AssetGroupId = (string)value; break;
						case "GroupName": this.str.GroupName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "Initial": this.str.Initial = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "AssetAccountId": this.str.AssetAccountId = (string)value; break;
						case "AssetSubLedgerId": this.str.AssetSubLedgerId = (string)value; break;
						case "AssetAccumulationAccountId": this.str.AssetAccumulationAccountId = (string)value; break;
						case "AssetAccumulationSubLedgerId": this.str.AssetAccumulationSubLedgerId = (string)value; break;
						case "AssetCostAccountId": this.str.AssetCostAccountId = (string)value; break;
						case "AssetCostSubLedgerId": this.str.AssetCostSubLedgerId = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserId": this.str.LastUpdateByUserId = (string)value; break;
						case "AssetCostDestructionAccountId": this.str.AssetCostDestructionAccountId = (string)value; break;
						case "AssetCostDestructionSubLedgerId": this.str.AssetCostDestructionSubLedgerId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "AssetAccountId":

							if (value == null || value is System.Int32)
								this.AssetAccountId = (System.Int32?)value;
							break;
						case "AssetSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetSubLedgerId = (System.Int32?)value;
							break;
						case "AssetAccumulationAccountId":

							if (value == null || value is System.Int32)
								this.AssetAccumulationAccountId = (System.Int32?)value;
							break;
						case "AssetAccumulationSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetAccumulationSubLedgerId = (System.Int32?)value;
							break;
						case "AssetCostAccountId":

							if (value == null || value is System.Int32)
								this.AssetCostAccountId = (System.Int32?)value;
							break;
						case "AssetCostSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetCostSubLedgerId = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "AssetCostDestructionAccountId":

							if (value == null || value is System.Int32)
								this.AssetCostDestructionAccountId = (System.Int32?)value;
							break;
						case "AssetCostDestructionSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetCostDestructionSubLedgerId = (System.Int32?)value;
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
		/// Maps to AssetGroup.AssetGroupId
		/// </summary>
		virtual public System.String AssetGroupId
		{
			get
			{
				return base.GetSystemString(AssetGroupMetadata.ColumnNames.AssetGroupId);
			}

			set
			{
				base.SetSystemString(AssetGroupMetadata.ColumnNames.AssetGroupId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.GroupName
		/// </summary>
		virtual public System.String GroupName
		{
			get
			{
				return base.GetSystemString(AssetGroupMetadata.ColumnNames.GroupName);
			}

			set
			{
				base.SetSystemString(AssetGroupMetadata.ColumnNames.GroupName, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(AssetGroupMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(AssetGroupMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.Initial
		/// </summary>
		virtual public System.String Initial
		{
			get
			{
				return base.GetSystemString(AssetGroupMetadata.ColumnNames.Initial);
			}

			set
			{
				base.SetSystemString(AssetGroupMetadata.ColumnNames.Initial, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AssetGroupMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(AssetGroupMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetAccountId
		/// </summary>
		virtual public System.Int32? AssetAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetAccumulationAccountId
		/// </summary>
		virtual public System.Int32? AssetAccumulationAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccumulationAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccumulationAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetAccumulationSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetAccumulationSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccumulationSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetAccumulationSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetCostAccountId
		/// </summary>
		virtual public System.Int32? AssetCostAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetCostSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetCostSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetGroupMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.LastUpdateByUserId
		/// </summary>
		virtual public System.String LastUpdateByUserId
		{
			get
			{
				return base.GetSystemString(AssetGroupMetadata.ColumnNames.LastUpdateByUserId);
			}

			set
			{
				base.SetSystemString(AssetGroupMetadata.ColumnNames.LastUpdateByUserId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetCostDestructionAccountId
		/// </summary>
		virtual public System.Int32? AssetCostDestructionAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostDestructionAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostDestructionAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetGroup.AssetCostDestructionSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetCostDestructionSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostDestructionSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetGroupMetadata.ColumnNames.AssetCostDestructionSubLedgerId, value);
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
			public esStrings(esAssetGroup entity)
			{
				this.entity = entity;
			}
			public System.String AssetGroupId
			{
				get
				{
					System.String data = entity.AssetGroupId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetGroupId = null;
					else entity.AssetGroupId = Convert.ToString(value);
				}
			}
			public System.String GroupName
			{
				get
				{
					System.String data = entity.GroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupName = null;
					else entity.GroupName = Convert.ToString(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			public System.String AssetAccountId
			{
				get
				{
					System.Int32? data = entity.AssetAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetAccountId = null;
					else entity.AssetAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetSubLedgerId = null;
					else entity.AssetSubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String AssetAccumulationAccountId
			{
				get
				{
					System.Int32? data = entity.AssetAccumulationAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetAccumulationAccountId = null;
					else entity.AssetAccumulationAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetAccumulationSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetAccumulationSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetAccumulationSubLedgerId = null;
					else entity.AssetAccumulationSubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String AssetCostAccountId
			{
				get
				{
					System.Int32? data = entity.AssetCostAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostAccountId = null;
					else entity.AssetCostAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetCostSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetCostSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostSubLedgerId = null;
					else entity.AssetCostSubLedgerId = Convert.ToInt32(value);
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
			public System.String LastUpdateByUserId
			{
				get
				{
					System.String data = entity.LastUpdateByUserId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserId = null;
					else entity.LastUpdateByUserId = Convert.ToString(value);
				}
			}
			public System.String AssetCostDestructionAccountId
			{
				get
				{
					System.Int32? data = entity.AssetCostDestructionAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostDestructionAccountId = null;
					else entity.AssetCostDestructionAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetCostDestructionSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetCostDestructionSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostDestructionSubLedgerId = null;
					else entity.AssetCostDestructionSubLedgerId = Convert.ToInt32(value);
				}
			}
			private esAssetGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetGroupQuery query)
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
				throw new Exception("esAssetGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetGroup : esAssetGroup
	{
	}

	[Serializable]
	abstract public class esAssetGroupQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetGroupMetadata.Meta();
			}
		}

		public esQueryItem AssetGroupId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetGroupId, esSystemType.String);
			}
		}

		public esQueryItem GroupName
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.GroupName, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem Initial
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.Initial, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem AssetAccountId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetAccumulationAccountId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetAccumulationAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetAccumulationSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetAccumulationSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetCostAccountId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetCostAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetCostSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetCostSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.LastUpdateByUserId, esSystemType.String);
			}
		}

		public esQueryItem AssetCostDestructionAccountId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetCostDestructionAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetCostDestructionSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetGroupMetadata.ColumnNames.AssetCostDestructionSubLedgerId, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetGroupCollection")]
	public partial class AssetGroupCollection : esAssetGroupCollection, IEnumerable<AssetGroup>
	{
		public AssetGroupCollection()
		{

		}

		public static implicit operator List<AssetGroup>(AssetGroupCollection coll)
		{
			List<AssetGroup> list = new List<AssetGroup>();

			foreach (AssetGroup emp in coll)
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
				return AssetGroupMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetGroup();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetGroupQuery();
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
		public bool Load(AssetGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetGroup AddNew()
		{
			AssetGroup entity = base.AddNewEntity() as AssetGroup;

			return entity;
		}
		public AssetGroup FindByPrimaryKey(String assetGroupId)
		{
			return base.FindByPrimaryKey(assetGroupId) as AssetGroup;
		}

		#region IEnumerable< AssetGroup> Members

		IEnumerator<AssetGroup> IEnumerable<AssetGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetGroup;
			}
		}

		#endregion

		private AssetGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetGroup ({AssetGroupId})")]
	[Serializable]
	public partial class AssetGroup : esAssetGroup
	{
		public AssetGroup()
		{
		}

		public AssetGroup(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetGroupMetadata.Meta();
			}
		}

		override protected esAssetGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetGroupQuery();
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
		public bool Load(AssetGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetGroupQuery : esAssetGroupQuery
	{
		public AssetGroupQuery()
		{

		}

		public AssetGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetGroupQuery";
		}
	}

	[Serializable]
	public partial class AssetGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetGroupId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetGroupId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.GroupName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetGroupMetadata.PropertyNames.GroupName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetGroupMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.Initial, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetGroupMetadata.PropertyNames.Initial;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetGroupMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetAccountId, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetSubLedgerId, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetSubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetAccumulationAccountId, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetAccumulationAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetAccumulationSubLedgerId, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetAccumulationSubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetCostAccountId, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetCostAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetCostSubLedgerId, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetCostSubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetGroupMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.LastUpdateByUserId, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetGroupMetadata.PropertyNames.LastUpdateByUserId;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetCostDestructionAccountId, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetCostDestructionAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetGroupMetadata.ColumnNames.AssetCostDestructionSubLedgerId, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetGroupMetadata.PropertyNames.AssetCostDestructionSubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetGroupMetadata Meta()
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
			public const string AssetGroupId = "AssetGroupId";
			public const string GroupName = "GroupName";
			public const string Description = "Description";
			public const string Initial = "Initial";
			public const string IsActive = "IsActive";
			public const string AssetAccountId = "AssetAccountId";
			public const string AssetSubLedgerId = "AssetSubLedgerId";
			public const string AssetAccumulationAccountId = "AssetAccumulationAccountId";
			public const string AssetAccumulationSubLedgerId = "AssetAccumulationSubLedgerId";
			public const string AssetCostAccountId = "AssetCostAccountId";
			public const string AssetCostSubLedgerId = "AssetCostSubLedgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserId = "LastUpdateByUserId";
			public const string AssetCostDestructionAccountId = "AssetCostDestructionAccountId";
			public const string AssetCostDestructionSubLedgerId = "AssetCostDestructionSubLedgerId";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssetGroupId = "AssetGroupId";
			public const string GroupName = "GroupName";
			public const string Description = "Description";
			public const string Initial = "Initial";
			public const string IsActive = "IsActive";
			public const string AssetAccountId = "AssetAccountId";
			public const string AssetSubLedgerId = "AssetSubLedgerId";
			public const string AssetAccumulationAccountId = "AssetAccumulationAccountId";
			public const string AssetAccumulationSubLedgerId = "AssetAccumulationSubLedgerId";
			public const string AssetCostAccountId = "AssetCostAccountId";
			public const string AssetCostSubLedgerId = "AssetCostSubLedgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserId = "LastUpdateByUserId";
			public const string AssetCostDestructionAccountId = "AssetCostDestructionAccountId";
			public const string AssetCostDestructionSubLedgerId = "AssetCostDestructionSubLedgerId";
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
			lock (typeof(AssetGroupMetadata))
			{
				if (AssetGroupMetadata.mapDelegates == null)
				{
					AssetGroupMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetGroupMetadata.meta == null)
				{
					AssetGroupMetadata.meta = new AssetGroupMetadata();
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

				meta.AddTypeMap("AssetGroupId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GroupName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Initial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssetAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetAccumulationAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetAccumulationSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetCostAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetCostSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetCostDestructionAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetCostDestructionSubLedgerId", new esTypeMap("int", "System.Int32"));


				meta.Source = "AssetGroup";
				meta.Destination = "AssetGroup";
				meta.spInsert = "proc_AssetGroupInsert";
				meta.spUpdate = "proc_AssetGroupUpdate";
				meta.spDelete = "proc_AssetGroupDelete";
				meta.spLoadAll = "proc_AssetGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetGroupLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
