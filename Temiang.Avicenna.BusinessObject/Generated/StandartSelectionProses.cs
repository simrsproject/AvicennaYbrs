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
	abstract public class esStandartSelectionProsesCollection : esEntityCollectionWAuditLog
	{
		public esStandartSelectionProsesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "StandartSelectionProsesCollection";
		}

		#region Query Logic
		protected void InitQuery(esStandartSelectionProsesQuery query)
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
			this.InitQuery(query as esStandartSelectionProsesQuery);
		}
		#endregion
		
		virtual public StandartSelectionProses DetachEntity(StandartSelectionProses entity)
		{
			return base.DetachEntity(entity) as StandartSelectionProses;
		}
		
		virtual public StandartSelectionProses AttachEntity(StandartSelectionProses entity)
		{
			return base.AttachEntity(entity) as StandartSelectionProses;
		}
		
		virtual public void Combine(StandartSelectionProsesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public StandartSelectionProses this[int index]
		{
			get
			{
				return base[index] as StandartSelectionProses;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(StandartSelectionProses);
		}
	}



	[Serializable]
	abstract public class esStandartSelectionProses : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esStandartSelectionProsesQuery GetDynamicQuery()
		{
			return null;
		}

		public esStandartSelectionProses()
		{

		}

		public esStandartSelectionProses(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 standartSelectionProsesId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standartSelectionProsesId);
			else
				return LoadByPrimaryKeyStoredProcedure(standartSelectionProsesId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 standartSelectionProsesId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(standartSelectionProsesId);
			else
				return LoadByPrimaryKeyStoredProcedure(standartSelectionProsesId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 standartSelectionProsesId)
		{
			esStandartSelectionProsesQuery query = this.GetDynamicQuery();
			query.Where(query.StandartSelectionProsesId == standartSelectionProsesId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 standartSelectionProsesId)
		{
			esParameters parms = new esParameters();
			parms.Add("StandartSelectionProsesId",standartSelectionProsesId);
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
						case "StandartSelectionProsesId": this.str.StandartSelectionProsesId = (string)value; break;							
						case "ProsesName": this.str.ProsesName = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "IsMandatory": this.str.IsMandatory = (string)value; break;							
						case "SRResultType": this.str.SRResultType = (string)value; break;							
						case "IsInternalCandidate": this.str.IsInternalCandidate = (string)value; break;							
						case "IsExsternalCandidate": this.str.IsExsternalCandidate = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StandartSelectionProsesId":
						
							if (value == null || value is System.Int32)
								this.StandartSelectionProsesId = (System.Int32?)value;
							break;
						
						case "IsMandatory":
						
							if (value == null || value is System.Boolean)
								this.IsMandatory = (System.Boolean?)value;
							break;
						
						case "IsInternalCandidate":
						
							if (value == null || value is System.Boolean)
								this.IsInternalCandidate = (System.Boolean?)value;
							break;
						
						case "IsExsternalCandidate":
						
							if (value == null || value is System.Boolean)
								this.IsExsternalCandidate = (System.Boolean?)value;
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
		/// Maps to StandartSelectionProses.StandartSelectionProsesId
		/// </summary>
		virtual public System.Int32? StandartSelectionProsesId
		{
			get
			{
				return base.GetSystemInt32(StandartSelectionProsesMetadata.ColumnNames.StandartSelectionProsesId);
			}
			
			set
			{
				base.SetSystemInt32(StandartSelectionProsesMetadata.ColumnNames.StandartSelectionProsesId, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.ProsesName
		/// </summary>
		virtual public System.String ProsesName
		{
			get
			{
				return base.GetSystemString(StandartSelectionProsesMetadata.ColumnNames.ProsesName);
			}
			
			set
			{
				base.SetSystemString(StandartSelectionProsesMetadata.ColumnNames.ProsesName, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(StandartSelectionProsesMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(StandartSelectionProsesMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.IsMandatory
		/// </summary>
		virtual public System.Boolean? IsMandatory
		{
			get
			{
				return base.GetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsMandatory);
			}
			
			set
			{
				base.SetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsMandatory, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.SRResultType
		/// </summary>
		virtual public System.String SRResultType
		{
			get
			{
				return base.GetSystemString(StandartSelectionProsesMetadata.ColumnNames.SRResultType);
			}
			
			set
			{
				base.SetSystemString(StandartSelectionProsesMetadata.ColumnNames.SRResultType, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.IsInternalCandidate
		/// </summary>
		virtual public System.Boolean? IsInternalCandidate
		{
			get
			{
				return base.GetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsInternalCandidate);
			}
			
			set
			{
				base.SetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsInternalCandidate, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.IsExsternalCandidate
		/// </summary>
		virtual public System.Boolean? IsExsternalCandidate
		{
			get
			{
				return base.GetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsExsternalCandidate);
			}
			
			set
			{
				base.SetSystemBoolean(StandartSelectionProsesMetadata.ColumnNames.IsExsternalCandidate, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(StandartSelectionProsesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(StandartSelectionProsesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to StandartSelectionProses.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(StandartSelectionProsesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(StandartSelectionProsesMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esStandartSelectionProses entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StandartSelectionProsesId
			{
				get
				{
					System.Int32? data = entity.StandartSelectionProsesId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandartSelectionProsesId = null;
					else entity.StandartSelectionProsesId = Convert.ToInt32(value);
				}
			}
				
			public System.String ProsesName
			{
				get
				{
					System.String data = entity.ProsesName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProsesName = null;
					else entity.ProsesName = Convert.ToString(value);
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
				
			public System.String IsMandatory
			{
				get
				{
					System.Boolean? data = entity.IsMandatory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMandatory = null;
					else entity.IsMandatory = Convert.ToBoolean(value);
				}
			}
				
			public System.String SRResultType
			{
				get
				{
					System.String data = entity.SRResultType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResultType = null;
					else entity.SRResultType = Convert.ToString(value);
				}
			}
				
			public System.String IsInternalCandidate
			{
				get
				{
					System.Boolean? data = entity.IsInternalCandidate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInternalCandidate = null;
					else entity.IsInternalCandidate = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsExsternalCandidate
			{
				get
				{
					System.Boolean? data = entity.IsExsternalCandidate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExsternalCandidate = null;
					else entity.IsExsternalCandidate = Convert.ToBoolean(value);
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
			

			private esStandartSelectionProses entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esStandartSelectionProsesQuery query)
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
				throw new Exception("esStandartSelectionProses can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class StandartSelectionProses : esStandartSelectionProses
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
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
	abstract public class esStandartSelectionProsesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StandartSelectionProsesMetadata.Meta();
			}
		}	
		

		public esQueryItem StandartSelectionProsesId
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.StandartSelectionProsesId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ProsesName
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.ProsesName, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem IsMandatory
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.IsMandatory, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SRResultType
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.SRResultType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsInternalCandidate
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.IsInternalCandidate, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsExsternalCandidate
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.IsExsternalCandidate, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, StandartSelectionProsesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("StandartSelectionProsesCollection")]
	public partial class StandartSelectionProsesCollection : esStandartSelectionProsesCollection, IEnumerable<StandartSelectionProses>
	{
		public StandartSelectionProsesCollection()
		{

		}
		
		public static implicit operator List<StandartSelectionProses>(StandartSelectionProsesCollection coll)
		{
			List<StandartSelectionProses> list = new List<StandartSelectionProses>();
			
			foreach (StandartSelectionProses emp in coll)
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
				return  StandartSelectionProsesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandartSelectionProsesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new StandartSelectionProses(row);
		}

		override protected esEntity CreateEntity()
		{
			return new StandartSelectionProses();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public StandartSelectionProsesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandartSelectionProsesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(StandartSelectionProsesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public StandartSelectionProses AddNew()
		{
			StandartSelectionProses entity = base.AddNewEntity() as StandartSelectionProses;
			
			return entity;
		}

		public StandartSelectionProses FindByPrimaryKey(System.Int32 standartSelectionProsesId)
		{
			return base.FindByPrimaryKey(standartSelectionProsesId) as StandartSelectionProses;
		}


		#region IEnumerable<StandartSelectionProses> Members

		IEnumerator<StandartSelectionProses> IEnumerable<StandartSelectionProses>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as StandartSelectionProses;
			}
		}

		#endregion
		
		private StandartSelectionProsesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'StandartSelectionProses' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("StandartSelectionProses ({StandartSelectionProsesId})")]
	[Serializable]
	public partial class StandartSelectionProses : esStandartSelectionProses
	{
		public StandartSelectionProses()
		{

		}
	
		public StandartSelectionProses(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StandartSelectionProsesMetadata.Meta();
			}
		}
		
		
		
		override protected esStandartSelectionProsesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StandartSelectionProsesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public StandartSelectionProsesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StandartSelectionProsesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(StandartSelectionProsesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private StandartSelectionProsesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class StandartSelectionProsesQuery : esStandartSelectionProsesQuery
	{
		public StandartSelectionProsesQuery()
		{

		}		
		
		public StandartSelectionProsesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "StandartSelectionProsesQuery";
        }
		
			
	}


	[Serializable]
	public partial class StandartSelectionProsesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StandartSelectionProsesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.StandartSelectionProsesId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.StandartSelectionProsesId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.ProsesName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.ProsesName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.IsMandatory, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.IsMandatory;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.SRResultType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.SRResultType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.IsInternalCandidate, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.IsInternalCandidate;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.IsExsternalCandidate, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.IsExsternalCandidate;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(StandartSelectionProsesMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = StandartSelectionProsesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public StandartSelectionProsesMetadata Meta()
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
			 public const string StandartSelectionProsesId = "StandartSelectionProsesId";
			 public const string ProsesName = "ProsesName";
			 public const string Description = "Description";
			 public const string IsMandatory = "IsMandatory";
			 public const string SRResultType = "SRResultType";
			 public const string IsInternalCandidate = "IsInternalCandidate";
			 public const string IsExsternalCandidate = "IsExsternalCandidate";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandartSelectionProsesId = "StandartSelectionProsesId";
			 public const string ProsesName = "ProsesName";
			 public const string Description = "Description";
			 public const string IsMandatory = "IsMandatory";
			 public const string SRResultType = "SRResultType";
			 public const string IsInternalCandidate = "IsInternalCandidate";
			 public const string IsExsternalCandidate = "IsExsternalCandidate";
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
			lock (typeof(StandartSelectionProsesMetadata))
			{
				if(StandartSelectionProsesMetadata.mapDelegates == null)
				{
					StandartSelectionProsesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StandartSelectionProsesMetadata.meta == null)
				{
					StandartSelectionProsesMetadata.meta = new StandartSelectionProsesMetadata();
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
				

				meta.AddTypeMap("StandartSelectionProsesId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ProsesName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMandatory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRResultType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInternalCandidate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsExsternalCandidate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "StandartSelectionProses";
				meta.Destination = "StandartSelectionProses";
				
				meta.spInsert = "proc_StandartSelectionProsesInsert";				
				meta.spUpdate = "proc_StandartSelectionProsesUpdate";		
				meta.spDelete = "proc_StandartSelectionProsesDelete";
				meta.spLoadAll = "proc_StandartSelectionProsesLoadAll";
				meta.spLoadByPrimaryKey = "proc_StandartSelectionProsesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StandartSelectionProsesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
