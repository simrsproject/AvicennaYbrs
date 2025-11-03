/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esSubLedgerGroupsCollection : esEntityCollectionWAuditLog
	{
		public esSubLedgerGroupsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SubLedgerGroupsCollection";
		}

		#region Query Logic
		protected void InitQuery(esSubLedgerGroupsQuery query)
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
			this.InitQuery(query as esSubLedgerGroupsQuery);
		}
		#endregion
		
		virtual public SubLedgerGroups DetachEntity(SubLedgerGroups entity)
		{
			return base.DetachEntity(entity) as SubLedgerGroups;
		}
		
		virtual public SubLedgerGroups AttachEntity(SubLedgerGroups entity)
		{
			return base.AttachEntity(entity) as SubLedgerGroups;
		}
		
		virtual public void Combine(SubLedgerGroupsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SubLedgerGroups this[int index]
		{
			get
			{
				return base[index] as SubLedgerGroups;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SubLedgerGroups);
		}
	}



	[Serializable]
	abstract public class esSubLedgerGroups : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSubLedgerGroupsQuery GetDynamicQuery()
		{
			return null;
		}

		public esSubLedgerGroups()
		{

		}

		public esSubLedgerGroups(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 subLedgerGroupId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerGroupId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerGroupId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 subLedgerGroupId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerGroupId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerGroupId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 subLedgerGroupId)
		{
			esSubLedgerGroupsQuery query = this.GetDynamicQuery();
			query.Where(query.SubLedgerGroupId == subLedgerGroupId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 subLedgerGroupId)
		{
			esParameters parms = new esParameters();
			parms.Add("SubLedgerGroupId",subLedgerGroupId);
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
						case "SubLedgerGroupId": this.str.SubLedgerGroupId = (string)value; break;							
						case "GroupCode": this.str.GroupCode = (string)value; break;							
						case "GroupName": this.str.GroupName = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "DateCreated": this.str.DateCreated = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "TempID": this.str.TempID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SubLedgerGroupId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerGroupId = (System.Int32?)value;
							break;
						
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "TempID":
						
							if (value == null || value is System.Int32)
								this.TempID = (System.Int32?)value;
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
		/// Maps to SubLedgerGroups.SubLedgerGroupId
		/// </summary>
		virtual public System.Int32? SubLedgerGroupId
		{
			get
			{
				return base.GetSystemInt32(SubLedgerGroupsMetadata.ColumnNames.SubLedgerGroupId);
			}
			
			set
			{
				base.SetSystemInt32(SubLedgerGroupsMetadata.ColumnNames.SubLedgerGroupId, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.GroupCode
		/// </summary>
		virtual public System.String GroupCode
		{
			get
			{
				return base.GetSystemString(SubLedgerGroupsMetadata.ColumnNames.GroupCode);
			}
			
			set
			{
				base.SetSystemString(SubLedgerGroupsMetadata.ColumnNames.GroupCode, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.GroupName
		/// </summary>
		virtual public System.String GroupName
		{
			get
			{
				return base.GetSystemString(SubLedgerGroupsMetadata.ColumnNames.GroupName);
			}
			
			set
			{
				base.SetSystemString(SubLedgerGroupsMetadata.ColumnNames.GroupName, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(SubLedgerGroupsMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(SubLedgerGroupsMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(SubLedgerGroupsMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(SubLedgerGroupsMetadata.ColumnNames.DateCreated, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SubLedgerGroupsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SubLedgerGroupsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(SubLedgerGroupsMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(SubLedgerGroupsMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SubLedgerGroupsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SubLedgerGroupsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerGroups.tempID
		/// </summary>
		virtual public System.Int32? TempID
		{
			get
			{
				return base.GetSystemInt32(SubLedgerGroupsMetadata.ColumnNames.TempID);
			}
			
			set
			{
				base.SetSystemInt32(SubLedgerGroupsMetadata.ColumnNames.TempID, value);
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
			public esStrings(esSubLedgerGroups entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SubLedgerGroupId
			{
				get
				{
					System.Int32? data = entity.SubLedgerGroupId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerGroupId = null;
					else entity.SubLedgerGroupId = Convert.ToInt32(value);
				}
			}
				
			public System.String GroupCode
			{
				get
				{
					System.String data = entity.GroupCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupCode = null;
					else entity.GroupCode = Convert.ToString(value);
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
				
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
				
			public System.String TempID
			{
				get
				{
					System.Int32? data = entity.TempID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TempID = null;
					else entity.TempID = Convert.ToInt32(value);
				}
			}
			

			private esSubLedgerGroups entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSubLedgerGroupsQuery query)
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
				throw new Exception("esSubLedgerGroups can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SubLedgerGroups : esSubLedgerGroups
	{

				
		#region SubLedgersCollectionByGroupId - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_SubLedgers_SubLedgerGroups
		/// </summary>

		[XmlIgnore]
		public SubLedgersCollection SubLedgersCollectionByGroupId
		{
			get
			{
				if(this._SubLedgersCollectionByGroupId == null)
				{
					this._SubLedgersCollectionByGroupId = new SubLedgersCollection();
					this._SubLedgersCollectionByGroupId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("SubLedgersCollectionByGroupId", this._SubLedgersCollectionByGroupId);
				
					if(this.SubLedgerGroupId != null)
					{
						this._SubLedgersCollectionByGroupId.Query.Where(this._SubLedgersCollectionByGroupId.Query.GroupId == this.SubLedgerGroupId);
						this._SubLedgersCollectionByGroupId.Query.Load();

						// Auto-hookup Foreign Keys
						this._SubLedgersCollectionByGroupId.fks.Add(SubLedgersMetadata.ColumnNames.GroupId, this.SubLedgerGroupId);
					}
				}

				return this._SubLedgersCollectionByGroupId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._SubLedgersCollectionByGroupId != null) 
				{ 
					this.RemovePostSave("SubLedgersCollectionByGroupId"); 
					this._SubLedgersCollectionByGroupId = null;
					
				} 
			} 			
		}

		private SubLedgersCollection _SubLedgersCollectionByGroupId;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "SubLedgersCollectionByGroupId", typeof(SubLedgersCollection), new SubLedgers()));
		
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
			if(this._SubLedgersCollectionByGroupId != null)
			{
				foreach(SubLedgers obj in this._SubLedgersCollectionByGroupId)
				{
					if(obj.es.IsAdded)
					{
						obj.GroupId = this.SubLedgerGroupId;
					}
				}
			}
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
	abstract public class esSubLedgerGroupsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SubLedgerGroupsMetadata.Meta();
			}
		}	
		

		public esQueryItem SubLedgerGroupId
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.SubLedgerGroupId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem GroupCode
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.GroupCode, esSystemType.String);
			}
		} 
		
		public esQueryItem GroupName
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.GroupName, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem TempID
		{
			get
			{
				return new esQueryItem(this, SubLedgerGroupsMetadata.ColumnNames.TempID, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SubLedgerGroupsCollection")]
	public partial class SubLedgerGroupsCollection : esSubLedgerGroupsCollection, IEnumerable<SubLedgerGroups>
	{
		public SubLedgerGroupsCollection()
		{

		}
		
		public static implicit operator List<SubLedgerGroups>(SubLedgerGroupsCollection coll)
		{
			List<SubLedgerGroups> list = new List<SubLedgerGroups>();
			
			foreach (SubLedgerGroups emp in coll)
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
				return  SubLedgerGroupsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgerGroupsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SubLedgerGroups(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SubLedgerGroups();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SubLedgerGroupsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgerGroupsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SubLedgerGroupsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SubLedgerGroups AddNew()
		{
			SubLedgerGroups entity = base.AddNewEntity() as SubLedgerGroups;
			
			return entity;
		}

		public SubLedgerGroups FindByPrimaryKey(System.Int32 subLedgerGroupId)
		{
			return base.FindByPrimaryKey(subLedgerGroupId) as SubLedgerGroups;
		}


		#region IEnumerable<SubLedgerGroups> Members

		IEnumerator<SubLedgerGroups> IEnumerable<SubLedgerGroups>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SubLedgerGroups;
			}
		}

		#endregion
		
		private SubLedgerGroupsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SubLedgerGroups' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SubLedgerGroups ({SubLedgerGroupId})")]
	[Serializable]
	public partial class SubLedgerGroups : esSubLedgerGroups
	{
		public SubLedgerGroups()
		{

		}
	
		public SubLedgerGroups(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SubLedgerGroupsMetadata.Meta();
			}
		}
		
		
		
		override protected esSubLedgerGroupsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgerGroupsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SubLedgerGroupsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgerGroupsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SubLedgerGroupsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SubLedgerGroupsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SubLedgerGroupsQuery : esSubLedgerGroupsQuery
	{
		public SubLedgerGroupsQuery()
		{

		}		
		
		public SubLedgerGroupsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SubLedgerGroupsQuery";
        }
		
			
	}


	[Serializable]
	public partial class SubLedgerGroupsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SubLedgerGroupsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.SubLedgerGroupId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.SubLedgerGroupId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.GroupCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.GroupCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.GroupName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.GroupName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.DateCreated, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.DateCreated;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.CreatedBy, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerGroupsMetadata.ColumnNames.TempID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgerGroupsMetadata.PropertyNames.TempID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SubLedgerGroupsMetadata Meta()
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
			 public const string SubLedgerGroupId = "SubLedgerGroupId";
			 public const string GroupCode = "GroupCode";
			 public const string GroupName = "GroupName";
			 public const string Description = "Description";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string TempID = "tempID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SubLedgerGroupId = "SubLedgerGroupId";
			 public const string GroupCode = "GroupCode";
			 public const string GroupName = "GroupName";
			 public const string Description = "Description";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string TempID = "TempID";
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
			lock (typeof(SubLedgerGroupsMetadata))
			{
				if(SubLedgerGroupsMetadata.mapDelegates == null)
				{
					SubLedgerGroupsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SubLedgerGroupsMetadata.meta == null)
				{
					SubLedgerGroupsMetadata.meta = new SubLedgerGroupsMetadata();
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
				

				meta.AddTypeMap("SubLedgerGroupId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GroupCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("GroupName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TempID", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "SubLedgerGroups";
				meta.Destination = "SubLedgerGroups";
				
				meta.spInsert = "proc_SubLedgerGroupsInsert";				
				meta.spUpdate = "proc_SubLedgerGroupsUpdate";		
				meta.spDelete = "proc_SubLedgerGroupsDelete";
				meta.spLoadAll = "proc_SubLedgerGroupsLoadAll";
				meta.spLoadByPrimaryKey = "proc_SubLedgerGroupsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SubLedgerGroupsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
