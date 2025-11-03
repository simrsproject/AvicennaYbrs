/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:12 PM
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
	abstract public class esAwardCollection : esEntityCollectionWAuditLog
	{
		public esAwardCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AwardCollection";
		}

		#region Query Logic
		protected void InitQuery(esAwardQuery query)
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
			this.InitQuery(query as esAwardQuery);
		}
		#endregion
		
		virtual public Award DetachEntity(Award entity)
		{
			return base.DetachEntity(entity) as Award;
		}
		
		virtual public Award AttachEntity(Award entity)
		{
			return base.AttachEntity(entity) as Award;
		}
		
		virtual public void Combine(AwardCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Award this[int index]
		{
			get
			{
				return base[index] as Award;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Award);
		}
	}



	[Serializable]
	abstract public class esAward : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAwardQuery GetDynamicQuery()
		{
			return null;
		}

		public esAward()
		{

		}

		public esAward(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 awardID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(awardID);
			else
				return LoadByPrimaryKeyStoredProcedure(awardID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 awardID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(awardID);
			else
				return LoadByPrimaryKeyStoredProcedure(awardID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 awardID)
		{
			esAwardQuery query = this.GetDynamicQuery();
			query.Where(query.AwardID == awardID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 awardID)
		{
			esParameters parms = new esParameters();
			parms.Add("AwardID",awardID);
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
						case "AwardID": this.str.AwardID = (string)value; break;							
						case "AwardCode": this.str.AwardCode = (string)value; break;							
						case "AwardName": this.str.AwardName = (string)value; break;							
						case "SRAwardCriteria": this.str.SRAwardCriteria = (string)value; break;							
						case "SRAwardType": this.str.SRAwardType = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "AwardPrize": this.str.AwardPrize = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AwardID":
						
							if (value == null || value is System.Int32)
								this.AwardID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to Award.AwardID
		/// </summary>
		virtual public System.Int32? AwardID
		{
			get
			{
				return base.GetSystemInt32(AwardMetadata.ColumnNames.AwardID);
			}
			
			set
			{
				base.SetSystemInt32(AwardMetadata.ColumnNames.AwardID, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.AwardCode
		/// </summary>
		virtual public System.String AwardCode
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.AwardCode);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.AwardCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.AwardName
		/// </summary>
		virtual public System.String AwardName
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.AwardName);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.AwardName, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.SRAwardCriteria
		/// </summary>
		virtual public System.String SRAwardCriteria
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.SRAwardCriteria);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.SRAwardCriteria, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.SRAwardType
		/// </summary>
		virtual public System.String SRAwardType
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.SRAwardType);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.SRAwardType, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(AwardMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(AwardMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(AwardMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(AwardMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.AwardPrize
		/// </summary>
		virtual public System.String AwardPrize
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.AwardPrize);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.AwardPrize, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AwardMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AwardMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Award.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AwardMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AwardMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAward entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AwardID
			{
				get
				{
					System.Int32? data = entity.AwardID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AwardID = null;
					else entity.AwardID = Convert.ToInt32(value);
				}
			}
				
			public System.String AwardCode
			{
				get
				{
					System.String data = entity.AwardCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AwardCode = null;
					else entity.AwardCode = Convert.ToString(value);
				}
			}
				
			public System.String AwardName
			{
				get
				{
					System.String data = entity.AwardName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AwardName = null;
					else entity.AwardName = Convert.ToString(value);
				}
			}
				
			public System.String SRAwardCriteria
			{
				get
				{
					System.String data = entity.SRAwardCriteria;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAwardCriteria = null;
					else entity.SRAwardCriteria = Convert.ToString(value);
				}
			}
				
			public System.String SRAwardType
			{
				get
				{
					System.String data = entity.SRAwardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAwardType = null;
					else entity.SRAwardType = Convert.ToString(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
				
			public System.String AwardPrize
			{
				get
				{
					System.String data = entity.AwardPrize;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AwardPrize = null;
					else entity.AwardPrize = Convert.ToString(value);
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
			

			private esAward entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAwardQuery query)
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
				throw new Exception("esAward can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Award : esAward
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
	abstract public class esAwardQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AwardMetadata.Meta();
			}
		}	
		

		public esQueryItem AwardID
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.AwardID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AwardCode
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.AwardCode, esSystemType.String);
			}
		} 
		
		public esQueryItem AwardName
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.AwardName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAwardCriteria
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.SRAwardCriteria, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAwardType
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.SRAwardType, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AwardPrize
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.AwardPrize, esSystemType.String);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AwardMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AwardCollection")]
	public partial class AwardCollection : esAwardCollection, IEnumerable<Award>
	{
		public AwardCollection()
		{

		}
		
		public static implicit operator List<Award>(AwardCollection coll)
		{
			List<Award> list = new List<Award>();
			
			foreach (Award emp in coll)
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
				return  AwardMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AwardQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Award(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Award();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AwardQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AwardQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AwardQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Award AddNew()
		{
			Award entity = base.AddNewEntity() as Award;
			
			return entity;
		}

		public Award FindByPrimaryKey(System.Int32 awardID)
		{
			return base.FindByPrimaryKey(awardID) as Award;
		}


		#region IEnumerable<Award> Members

		IEnumerator<Award> IEnumerable<Award>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Award;
			}
		}

		#endregion
		
		private AwardQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Award' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Award ({AwardID})")]
	[Serializable]
	public partial class Award : esAward
	{
		public Award()
		{

		}
	
		public Award(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AwardMetadata.Meta();
			}
		}
		
		
		
		override protected esAwardQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AwardQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AwardQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AwardQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AwardQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AwardQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AwardQuery : esAwardQuery
	{
		public AwardQuery()
		{

		}		
		
		public AwardQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AwardQuery";
        }
		
			
	}


	[Serializable]
	public partial class AwardMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AwardMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AwardMetadata.ColumnNames.AwardID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AwardMetadata.PropertyNames.AwardID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.AwardCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.AwardCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.AwardName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.AwardName;
			c.CharacterMaxLength = 400;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.SRAwardCriteria, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.SRAwardCriteria;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.SRAwardType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.SRAwardType;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.ValidFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AwardMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.ValidTo, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AwardMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.AwardPrize, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.AwardPrize;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AwardMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AwardMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AwardMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AwardMetadata Meta()
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
			 public const string AwardID = "AwardID";
			 public const string AwardCode = "AwardCode";
			 public const string AwardName = "AwardName";
			 public const string SRAwardCriteria = "SRAwardCriteria";
			 public const string SRAwardType = "SRAwardType";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string AwardPrize = "AwardPrize";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AwardID = "AwardID";
			 public const string AwardCode = "AwardCode";
			 public const string AwardName = "AwardName";
			 public const string SRAwardCriteria = "SRAwardCriteria";
			 public const string SRAwardType = "SRAwardType";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string AwardPrize = "AwardPrize";
			 public const string Note = "Note";
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
			lock (typeof(AwardMetadata))
			{
				if(AwardMetadata.mapDelegates == null)
				{
					AwardMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AwardMetadata.meta == null)
				{
					AwardMetadata.meta = new AwardMetadata();
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
				

				meta.AddTypeMap("AwardID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AwardCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("AwardName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAwardCriteria", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SRAwardType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("AwardPrize", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Award";
				meta.Destination = "Award";
				
				meta.spInsert = "proc_AwardInsert";				
				meta.spUpdate = "proc_AwardUpdate";		
				meta.spDelete = "proc_AwardDelete";
				meta.spLoadAll = "proc_AwardLoadAll";
				meta.spLoadByPrimaryKey = "proc_AwardLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AwardMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
