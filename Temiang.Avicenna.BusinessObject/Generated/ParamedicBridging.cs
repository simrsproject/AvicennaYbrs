/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/23/2018 2:26:06 AM
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
	abstract public class esParamedicBridgingCollection : esEntityCollectionWAuditLog
	{
		public esParamedicBridgingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicBridgingCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicBridgingQuery query)
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
			this.InitQuery(query as esParamedicBridgingQuery);
		}
		#endregion
		
		virtual public ParamedicBridging DetachEntity(ParamedicBridging entity)
		{
			return base.DetachEntity(entity) as ParamedicBridging;
		}
		
		virtual public ParamedicBridging AttachEntity(ParamedicBridging entity)
		{
			return base.AttachEntity(entity) as ParamedicBridging;
		}
		
		virtual public void Combine(ParamedicBridgingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicBridging this[int index]
		{
			get
			{
				return base[index] as ParamedicBridging;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicBridging);
		}
	}



	[Serializable]
	abstract public class esParamedicBridging : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicBridgingQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicBridging()
		{

		}

		public esParamedicBridging(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String sRBridgingType, System.String bridgingID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, sRBridgingType, bridgingID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, sRBridgingType, bridgingID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String sRBridgingType, System.String bridgingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, sRBridgingType, bridgingID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, sRBridgingType, bridgingID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String sRBridgingType, System.String bridgingID)
		{
			esParamedicBridgingQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.SRBridgingType == sRBridgingType, query.BridgingID == bridgingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String sRBridgingType, System.String bridgingID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);			parms.Add("SRBridgingType",sRBridgingType);			parms.Add("BridgingID",bridgingID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;							
						case "BridgingID": this.str.BridgingID = (string)value; break;							
						case "BridgingName": this.str.BridgingName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "SpecialisticID": this.str.SpecialisticID = (string)value; break;							
						case "DutyType": this.str.DutyType = (string)value; break;
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
		/// Maps to ParamedicBridging.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.SRBridgingType);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.BridgingID
		/// </summary>
		virtual public System.String BridgingID
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.BridgingID);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.BridgingID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.BridgingName
		/// </summary>
		virtual public System.String BridgingName
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.BridgingName);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.BridgingName, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ParamedicBridgingMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicBridgingMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicBridgingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.SpecialisticID
		/// </summary>
		virtual public System.String SpecialisticID
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.SpecialisticID);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.SpecialisticID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicBridging.DutyType
		/// </summary>
		virtual public System.String DutyType
		{
			get
			{
				return base.GetSystemString(ParamedicBridgingMetadata.ColumnNames.DutyType);
			}
			
			set
			{
				base.SetSystemString(ParamedicBridgingMetadata.ColumnNames.DutyType, value);
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
			public esStrings(esParamedicBridging entity)
			{
				this.entity = entity;
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
				
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
				}
			}
				
			public System.String BridgingID
			{
				get
				{
					System.String data = entity.BridgingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingID = null;
					else entity.BridgingID = Convert.ToString(value);
				}
			}
				
			public System.String BridgingName
			{
				get
				{
					System.String data = entity.BridgingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingName = null;
					else entity.BridgingName = Convert.ToString(value);
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
				
			public System.String SpecialisticID
			{
				get
				{
					System.String data = entity.SpecialisticID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialisticID = null;
					else entity.SpecialisticID = Convert.ToString(value);
				}
			}
				
			public System.String DutyType
			{
				get
				{
					System.String data = entity.DutyType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DutyType = null;
					else entity.DutyType = Convert.ToString(value);
				}
			}
			

			private esParamedicBridging entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicBridgingQuery query)
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
				throw new Exception("esParamedicBridging can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicBridging : esParamedicBridging
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
	abstract public class esParamedicBridgingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicBridgingMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		} 
		
		public esQueryItem BridgingID
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
			}
		} 
		
		public esQueryItem BridgingName
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.BridgingName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialisticID
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.SpecialisticID, esSystemType.String);
			}
		} 
		
		public esQueryItem DutyType
		{
			get
			{
				return new esQueryItem(this, ParamedicBridgingMetadata.ColumnNames.DutyType, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicBridgingCollection")]
	public partial class ParamedicBridgingCollection : esParamedicBridgingCollection, IEnumerable<ParamedicBridging>
	{
		public ParamedicBridgingCollection()
		{

		}
		
		public static implicit operator List<ParamedicBridging>(ParamedicBridgingCollection coll)
		{
			List<ParamedicBridging> list = new List<ParamedicBridging>();
			
			foreach (ParamedicBridging emp in coll)
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
				return  ParamedicBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicBridging(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicBridging();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicBridging AddNew()
		{
			ParamedicBridging entity = base.AddNewEntity() as ParamedicBridging;
			
			return entity;
		}

		public ParamedicBridging FindByPrimaryKey(System.String paramedicID, System.String sRBridgingType, System.String bridgingID)
		{
			return base.FindByPrimaryKey(paramedicID, sRBridgingType, bridgingID) as ParamedicBridging;
		}


		#region IEnumerable<ParamedicBridging> Members

		IEnumerator<ParamedicBridging> IEnumerable<ParamedicBridging>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicBridging;
			}
		}

		#endregion
		
		private ParamedicBridgingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicBridging' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicBridging ({ParamedicID},{SRBridgingType},{BridgingID})")]
	[Serializable]
	public partial class ParamedicBridging : esParamedicBridging
	{
		public ParamedicBridging()
		{

		}
	
		public ParamedicBridging(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicBridgingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicBridgingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicBridgingQuery : esParamedicBridgingQuery
	{
		public ParamedicBridgingQuery()
		{

		}		
		
		public ParamedicBridgingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicBridgingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicBridgingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicBridgingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.SRBridgingType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.SRBridgingType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.BridgingID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.BridgingID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.BridgingName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.BridgingName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.SpecialisticID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.SpecialisticID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicBridgingMetadata.ColumnNames.DutyType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicBridgingMetadata.PropertyNames.DutyType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicBridgingMetadata Meta()
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
			 public const string ParamedicID = "ParamedicID";
			 public const string SRBridgingType = "SRBridgingType";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingName = "BridgingName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SpecialisticID = "SpecialisticID";
			 public const string DutyType = "DutyType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParamedicID = "ParamedicID";
			 public const string SRBridgingType = "SRBridgingType";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingName = "BridgingName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SpecialisticID = "SpecialisticID";
			 public const string DutyType = "DutyType";
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
			lock (typeof(ParamedicBridgingMetadata))
			{
				if(ParamedicBridgingMetadata.mapDelegates == null)
				{
					ParamedicBridgingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicBridgingMetadata.meta == null)
				{
					ParamedicBridgingMetadata.meta = new ParamedicBridgingMetadata();
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
				

				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialisticID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DutyType", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicBridging";
				meta.Destination = "ParamedicBridging";
				
				meta.spInsert = "proc_ParamedicBridgingInsert";				
				meta.spUpdate = "proc_ParamedicBridgingUpdate";		
				meta.spDelete = "proc_ParamedicBridgingDelete";
				meta.spLoadAll = "proc_ParamedicBridgingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicBridgingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicBridgingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
