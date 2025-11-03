/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esEmployeeAchievementCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeAchievementCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeAchievementCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeAchievementQuery query)
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
			this.InitQuery(query as esEmployeeAchievementQuery);
		}
		#endregion
		
		virtual public EmployeeAchievement DetachEntity(EmployeeAchievement entity)
		{
			return base.DetachEntity(entity) as EmployeeAchievement;
		}
		
		virtual public EmployeeAchievement AttachEntity(EmployeeAchievement entity)
		{
			return base.AttachEntity(entity) as EmployeeAchievement;
		}
		
		virtual public void Combine(EmployeeAchievementCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeAchievement this[int index]
		{
			get
			{
				return base[index] as EmployeeAchievement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeAchievement);
		}
	}



	[Serializable]
	abstract public class esEmployeeAchievement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeAchievementQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeAchievement()
		{

		}

		public esEmployeeAchievement(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeAchievementID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeAchievementID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeAchievementID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeAchievementID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeAchievementID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeAchievementID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeAchievementID)
		{
			esEmployeeAchievementQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeAchievementID == employeeAchievementID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeAchievementID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeAchievementID",employeeAchievementID);
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
						case "EmployeeAchievementID": this.str.EmployeeAchievementID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "AwardID": this.str.AwardID = (string)value; break;							
						case "AwardDate": this.str.AwardDate = (string)value; break;							
						case "Achievement": this.str.Achievement = (string)value; break;							
						case "FinancialValue": this.str.FinancialValue = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeAchievementID":
						
							if (value == null || value is System.Int32)
								this.EmployeeAchievementID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "AwardID":
						
							if (value == null || value is System.Int32)
								this.AwardID = (System.Int32?)value;
							break;
						
						case "AwardDate":
						
							if (value == null || value is System.DateTime)
								this.AwardDate = (System.DateTime?)value;
							break;
						
						case "FinancialValue":
						
							if (value == null || value is System.Decimal)
								this.FinancialValue = (System.Decimal?)value;
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
		/// Maps to EmployeeAchievement.EmployeeAchievementID
		/// </summary>
		virtual public System.Int32? EmployeeAchievementID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAchievementMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeAchievementMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.AwardID
		/// </summary>
		virtual public System.Int32? AwardID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAchievementMetadata.ColumnNames.AwardID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeAchievementMetadata.ColumnNames.AwardID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.AwardDate
		/// </summary>
		virtual public System.DateTime? AwardDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAchievementMetadata.ColumnNames.AwardDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeAchievementMetadata.ColumnNames.AwardDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.Achievement
		/// </summary>
		virtual public System.String Achievement
		{
			get
			{
				return base.GetSystemString(EmployeeAchievementMetadata.ColumnNames.Achievement);
			}
			
			set
			{
				base.SetSystemString(EmployeeAchievementMetadata.ColumnNames.Achievement, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.FinancialValue
		/// </summary>
		virtual public System.Decimal? FinancialValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeAchievementMetadata.ColumnNames.FinancialValue);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeAchievementMetadata.ColumnNames.FinancialValue, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeAchievementMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(EmployeeAchievementMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAchievementMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeAchievementMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeAchievement.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAchievementMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeAchievementMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeAchievement entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeAchievementID
			{
				get
				{
					System.Int32? data = entity.EmployeeAchievementID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeAchievementID = null;
					else entity.EmployeeAchievementID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
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
				
			public System.String AwardDate
			{
				get
				{
					System.DateTime? data = entity.AwardDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AwardDate = null;
					else entity.AwardDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Achievement
			{
				get
				{
					System.String data = entity.Achievement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Achievement = null;
					else entity.Achievement = Convert.ToString(value);
				}
			}
				
			public System.String FinancialValue
			{
				get
				{
					System.Decimal? data = entity.FinancialValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinancialValue = null;
					else entity.FinancialValue = Convert.ToDecimal(value);
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
			

			private esEmployeeAchievement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeAchievementQuery query)
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
				throw new Exception("esEmployeeAchievement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeAchievement : esEmployeeAchievement
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
	abstract public class esEmployeeAchievementQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAchievementMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeAchievementID
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AwardID
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.AwardID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AwardDate
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.AwardDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Achievement
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.Achievement, esSystemType.String);
			}
		} 
		
		public esQueryItem FinancialValue
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.FinancialValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAchievementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeAchievementCollection")]
	public partial class EmployeeAchievementCollection : esEmployeeAchievementCollection, IEnumerable<EmployeeAchievement>
	{
		public EmployeeAchievementCollection()
		{

		}
		
		public static implicit operator List<EmployeeAchievement>(EmployeeAchievementCollection coll)
		{
			List<EmployeeAchievement> list = new List<EmployeeAchievement>();
			
			foreach (EmployeeAchievement emp in coll)
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
				return  EmployeeAchievementMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAchievementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeAchievement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeAchievement();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeAchievementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAchievementQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeAchievementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeAchievement AddNew()
		{
			EmployeeAchievement entity = base.AddNewEntity() as EmployeeAchievement;
			
			return entity;
		}

		public EmployeeAchievement FindByPrimaryKey(System.Int32 employeeAchievementID)
		{
			return base.FindByPrimaryKey(employeeAchievementID) as EmployeeAchievement;
		}


		#region IEnumerable<EmployeeAchievement> Members

		IEnumerator<EmployeeAchievement> IEnumerable<EmployeeAchievement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeAchievement;
			}
		}

		#endregion
		
		private EmployeeAchievementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeAchievement' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeAchievement ({EmployeeAchievementID})")]
	[Serializable]
	public partial class EmployeeAchievement : esEmployeeAchievement
	{
		public EmployeeAchievement()
		{

		}
	
		public EmployeeAchievement(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAchievementMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeAchievementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAchievementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeAchievementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAchievementQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeAchievementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeAchievementQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeAchievementQuery : esEmployeeAchievementQuery
	{
		public EmployeeAchievementQuery()
		{

		}		
		
		public EmployeeAchievementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeAchievementQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeAchievementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeAchievementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.EmployeeAchievementID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.EmployeeAchievementID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.AwardID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.AwardID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.AwardDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.AwardDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.Achievement, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.Achievement;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.FinancialValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.FinancialValue;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.Note, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeAchievementMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAchievementMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeAchievementMetadata Meta()
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
			 public const string EmployeeAchievementID = "EmployeeAchievementID";
			 public const string PersonID = "PersonID";
			 public const string AwardID = "AwardID";
			 public const string AwardDate = "AwardDate";
			 public const string Achievement = "Achievement";
			 public const string FinancialValue = "FinancialValue";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeAchievementID = "EmployeeAchievementID";
			 public const string PersonID = "PersonID";
			 public const string AwardID = "AwardID";
			 public const string AwardDate = "AwardDate";
			 public const string Achievement = "Achievement";
			 public const string FinancialValue = "FinancialValue";
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
			lock (typeof(EmployeeAchievementMetadata))
			{
				if(EmployeeAchievementMetadata.mapDelegates == null)
				{
					EmployeeAchievementMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeAchievementMetadata.meta == null)
				{
					EmployeeAchievementMetadata.meta = new EmployeeAchievementMetadata();
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
				

				meta.AddTypeMap("EmployeeAchievementID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AwardID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AwardDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Achievement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FinancialValue", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeAchievement";
				meta.Destination = "EmployeeAchievement";
				
				meta.spInsert = "proc_EmployeeAchievementInsert";				
				meta.spUpdate = "proc_EmployeeAchievementUpdate";		
				meta.spDelete = "proc_EmployeeAchievementDelete";
				meta.spLoadAll = "proc_EmployeeAchievementLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeAchievementLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeAchievementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
