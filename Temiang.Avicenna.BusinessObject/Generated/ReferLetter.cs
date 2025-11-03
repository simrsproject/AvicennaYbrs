///*
//===============================================================================
//                 Persistence Layer and Business Objects
//===============================================================================
//Version         : 2009.2.1214.0
//Driver          : SQL
//Date Generated  : 10/1/2014 3:54:20 PM
//===============================================================================
//*/

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using System.ComponentModel;
//using System.Xml.Serialization;


//using Temiang.Dal.Core;
//using Temiang.Dal.Interfaces;
//using Temiang.Dal.DynamicQuery;



//namespace Temiang.Avicenna.BusinessObject
//{

//	[Serializable]
//	abstract public class esReferLetterCollection : esEntityCollectionWAuditLog
//	{
//		public esReferLetterCollection()
//		{

//		}

//		protected override string GetCollectionName()
//		{
//			return "ReferLetterCollection";
//		}

//		#region Query Logic
//		protected void InitQuery(esReferLetterQuery query)
//		{
//			query.OnLoadDelegate = this.OnQueryLoaded;
//			query.es2.Connection = ((IEntityCollection)this).Connection;
//		}

//		protected bool OnQueryLoaded(DataTable table)
//		{
//			this.PopulateCollection(table);
//			return (this.RowCount > 0) ? true : false;
//		}
		
//		protected override void HookupQuery(esDynamicQuery query)
//		{
//			this.InitQuery(query as esReferLetterQuery);
//		}
//		#endregion
		
//		virtual public ReferLetter DetachEntity(ReferLetter entity)
//		{
//			return base.DetachEntity(entity) as ReferLetter;
//		}
		
//		virtual public ReferLetter AttachEntity(ReferLetter entity)
//		{
//			return base.AttachEntity(entity) as ReferLetter;
//		}
		
//		virtual public void Combine(ReferLetterCollection collection)
//		{
//			base.Combine(collection);
//		}
		
//		new public ReferLetter this[int index]
//		{
//			get
//			{
//				return base[index] as ReferLetter;
//			}
//		}

//		public override Type GetEntityType()
//		{
//			return typeof(ReferLetter);
//		}
//	}



//	[Serializable]
//	abstract public class esReferLetter : esEntityWAuditLog
//	{
//		/// <summary>
//		/// Used internally by the entity's DynamicQuery mechanism.
//		/// </summary>
//		virtual protected esReferLetterQuery GetDynamicQuery()
//		{
//			return null;
//		}

//		public esReferLetter()
//		{

//		}

//		public esReferLetter(DataRow row)
//			: base(row)
//		{

//		}
		
//		#region LoadByPrimaryKey
//		public virtual bool LoadByPrimaryKey(System.String regApptNo)
//		{
//			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
//				return LoadByPrimaryKeyDynamic(regApptNo);
//			else
//				return LoadByPrimaryKeyStoredProcedure(regApptNo);
//		}

//		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String regApptNo)
//		{
//			if (sqlAccessType == esSqlAccessType.DynamicSQL)
//				return LoadByPrimaryKeyDynamic(regApptNo);
//			else
//				return LoadByPrimaryKeyStoredProcedure(regApptNo);
//		}

//		private bool LoadByPrimaryKeyDynamic(System.String regApptNo)
//		{
//			esReferLetterQuery query = this.GetDynamicQuery();
//			query.Where(query.RegApptNo == regApptNo);
//			return query.Load();
//		}

//		private bool LoadByPrimaryKeyStoredProcedure(System.String regApptNo)
//		{
//			esParameters parms = new esParameters();
//			parms.Add("RegApptNo",regApptNo);
//			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
//		}
//		#endregion
		
		
		
//		#region Properties
		
		
//		public override void SetProperties(IDictionary values)
//		{
//			foreach (string propertyName in values.Keys)
//			{
//				this.SetProperty(propertyName, values[propertyName]);
//			}
//		}

//		public override void SetProperty(string name, object value)
//		{
//			if(this.Row == null) this.AddNew();
			
//			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
//			if (col != null)
//			{
//				if(value == null || value is System.String)
//				{				
//					// Use the strongly typed property
//					switch (name)
//					{							
//						case "RegApptNo": this.str.RegApptNo = (string)value; break;							
//						case "FromRegistrationNo": this.str.FromRegistrationNo = (string)value; break;							
//						case "ActionExamTreatment": this.str.ActionExamTreatment = (string)value; break;							
//						case "Notes": this.str.Notes = (string)value; break;							
//						case "Answer": this.str.Answer = (string)value; break;							
//						case "IsAppointment": this.str.IsAppointment = (string)value; break;							
//						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
//						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
//					}
//				}
//				else
//				{
//					switch (name)
//					{	
//						case "IsAppointment":
						
//							if (value == null || value is System.Boolean)
//								this.IsAppointment = (System.Boolean?)value;
//							break;
						
//						case "LastUpdateDateTime":
						
//							if (value == null || value is System.DateTime)
//								this.LastUpdateDateTime = (System.DateTime?)value;
//							break;
					

//						default:
//							break;
//					}
//				}
//			}
//			else if(this.Row.Table.Columns.Contains(name))
//			{
//				this.Row[name] = value;
//			}
//			else
//			{
//				throw new Exception("SetProperty Error: '" + name + "' not found");
//			}
//		}
		
		
//		/// <summary>
//		/// Maps to ReferLetter.RegApptNo
//		/// </summary>
//		virtual public System.String RegApptNo
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.RegApptNo);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.RegApptNo, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.FromRegistrationNo
//		/// </summary>
//		virtual public System.String FromRegistrationNo
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.FromRegistrationNo);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.FromRegistrationNo, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.ActionExamTreatment
//		/// </summary>
//		virtual public System.String ActionExamTreatment
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.ActionExamTreatment);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.ActionExamTreatment, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.Notes
//		/// </summary>
//		virtual public System.String Notes
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.Notes);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.Notes, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.Answer
//		/// </summary>
//		virtual public System.String Answer
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.Answer);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.Answer, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.IsAppointment
//		/// </summary>
//		virtual public System.Boolean? IsAppointment
//		{
//			get
//			{
//				return base.GetSystemBoolean(ReferLetterMetadata.ColumnNames.IsAppointment);
//			}
			
//			set
//			{
//				base.SetSystemBoolean(ReferLetterMetadata.ColumnNames.IsAppointment, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.LastUpdateDateTime
//		/// </summary>
//		virtual public System.DateTime? LastUpdateDateTime
//		{
//			get
//			{
//				return base.GetSystemDateTime(ReferLetterMetadata.ColumnNames.LastUpdateDateTime);
//			}
			
//			set
//			{
//				base.SetSystemDateTime(ReferLetterMetadata.ColumnNames.LastUpdateDateTime, value);
//			}
//		}
		
//		/// <summary>
//		/// Maps to ReferLetter.LastUpdateByUserID
//		/// </summary>
//		virtual public System.String LastUpdateByUserID
//		{
//			get
//			{
//				return base.GetSystemString(ReferLetterMetadata.ColumnNames.LastUpdateByUserID);
//			}
			
//			set
//			{
//				base.SetSystemString(ReferLetterMetadata.ColumnNames.LastUpdateByUserID, value);
//			}
//		}
		
//		#endregion	

//		#region String Properties


//		[BrowsableAttribute( false )]
//		public esStrings str
//		{
//			get
//			{
//				if (esstrings == null)
//				{
//					esstrings = new esStrings(this);
//				}
//				return esstrings;
//			}
//		}


//		[Serializable]
//		sealed public class esStrings
//		{
//			public esStrings(esReferLetter entity)
//			{
//				this.entity = entity;
//			}
			
	
//			public System.String RegApptNo
//			{
//				get
//				{
//					System.String data = entity.RegApptNo;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.RegApptNo = null;
//					else entity.RegApptNo = Convert.ToString(value);
//				}
//			}
				
//			public System.String FromRegistrationNo
//			{
//				get
//				{
//					System.String data = entity.FromRegistrationNo;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.FromRegistrationNo = null;
//					else entity.FromRegistrationNo = Convert.ToString(value);
//				}
//			}
				
//			public System.String ActionExamTreatment
//			{
//				get
//				{
//					System.String data = entity.ActionExamTreatment;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.ActionExamTreatment = null;
//					else entity.ActionExamTreatment = Convert.ToString(value);
//				}
//			}
				
//			public System.String Notes
//			{
//				get
//				{
//					System.String data = entity.Notes;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.Notes = null;
//					else entity.Notes = Convert.ToString(value);
//				}
//			}
				
//			public System.String Answer
//			{
//				get
//				{
//					System.String data = entity.Answer;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.Answer = null;
//					else entity.Answer = Convert.ToString(value);
//				}
//			}
				
//			public System.String IsAppointment
//			{
//				get
//				{
//					System.Boolean? data = entity.IsAppointment;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.IsAppointment = null;
//					else entity.IsAppointment = Convert.ToBoolean(value);
//				}
//			}
				
//			public System.String LastUpdateDateTime
//			{
//				get
//				{
//					System.DateTime? data = entity.LastUpdateDateTime;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
//					else entity.LastUpdateDateTime = Convert.ToDateTime(value);
//				}
//			}
				
//			public System.String LastUpdateByUserID
//			{
//				get
//				{
//					System.String data = entity.LastUpdateByUserID;
//					return (data == null) ? String.Empty : Convert.ToString(data);
//				}

//				set
//				{
//					if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
//					else entity.LastUpdateByUserID = Convert.ToString(value);
//				}
//			}
			

//			private esReferLetter entity;
//		}
//		#endregion

//		#region Query Logic
//		protected void InitQuery(esReferLetterQuery query)
//		{
//			query.OnLoadDelegate = this.OnQueryLoaded;
//			query.es2.Connection = ((IEntity)this).Connection;
//		}

//		[System.Diagnostics.DebuggerNonUserCode]
//		protected bool OnQueryLoaded(DataTable table)
//		{
//			bool dataFound = this.PopulateEntity(table);

//			if (this.RowCount > 1)
//			{
//				throw new Exception("esReferLetter can only hold one record of data");
//			}

//			return dataFound;
//		}
//		#endregion
		
//		[NonSerialized]
//		private esStrings esstrings;
//	}


	
//	public partial class ReferLetter : esReferLetter
//	{

		
//		/// <summary>
//		/// Used internally by the entity's hierarchical properties.
//		/// </summary>
//		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
//		{
//			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
//			return props;
//		}	
		
//		/// <summary>
//		/// Used internally for retrieving AutoIncrementing keys
//		/// during hierarchical PreSave.
//		/// </summary>
//		protected override void ApplyPreSaveKeys()
//		{
//		}
		
//		/// <summary>
//		/// Used internally for retrieving AutoIncrementing keys
//		/// during hierarchical PostSave.
//		/// </summary>
//		protected override void ApplyPostSaveKeys()
//		{
//		}
		
//		/// <summary>
//		/// Used internally for retrieving AutoIncrementing keys
//		/// during hierarchical PostOneToOneSave.
//		/// </summary>
//		protected override void ApplyPostOneSaveKeys()
//		{
//		}
		
//	}



//	[Serializable]
//	abstract public class esReferLetterQuery : esDynamicQuery
//	{
//		override protected IMetadata Meta
//		{
//			get
//			{
//				return ReferLetterMetadata.Meta();
//			}
//		}	
		

//		public esQueryItem RegApptNo
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.RegApptNo, esSystemType.String);
//			}
//		} 
		
//		public esQueryItem FromRegistrationNo
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.FromRegistrationNo, esSystemType.String);
//			}
//		} 
		
//		public esQueryItem ActionExamTreatment
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.ActionExamTreatment, esSystemType.String);
//			}
//		} 
		
//		public esQueryItem Notes
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.Notes, esSystemType.String);
//			}
//		} 
		
//		public esQueryItem Answer
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.Answer, esSystemType.String);
//			}
//		} 
		
//		public esQueryItem IsAppointment
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.IsAppointment, esSystemType.Boolean);
//			}
//		} 
		
//		public esQueryItem LastUpdateDateTime
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
//			}
//		} 
		
//		public esQueryItem LastUpdateByUserID
//		{
//			get
//			{
//				return new esQueryItem(this, ReferLetterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
//			}
//		} 
		
//	}



//    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
//	[Serializable]
//	[XmlType("ReferLetterCollection")]
//	public partial class ReferLetterCollection : esReferLetterCollection, IEnumerable<ReferLetter>
//	{
//		public ReferLetterCollection()
//		{

//		}
		
//		public static implicit operator List<ReferLetter>(ReferLetterCollection coll)
//		{
//			List<ReferLetter> list = new List<ReferLetter>();
			
//			foreach (ReferLetter emp in coll)
//			{
//				list.Add(emp);
//			}
			
//			return list;
//		}
		
//		#region Housekeeping methods
//		override protected IMetadata Meta
//		{
//			get
//			{
//				return  ReferLetterMetadata.Meta();
//			}
//		}
		
		
		
//		override protected esDynamicQuery GetDynamicQuery()
//		{
//			if (this.query == null)
//			{
//				this.query = new ReferLetterQuery();
//				this.InitQuery(query);
//			}
//			return this.query;
//		}
		
//		override protected esEntity CreateEntityForCollection(DataRow row)
//		{
//			return new ReferLetter(row);
//		}

//		override protected esEntity CreateEntity()
//		{
//			return new ReferLetter();
//		}
		
		
//		#endregion


//		[BrowsableAttribute( false )]
//		public ReferLetterQuery Query
//		{
//			get
//			{
//				if (this.query == null)
//				{
//					this.query = new ReferLetterQuery();
//					base.InitQuery(this.query);
//				}

//				return this.query;
//			}
//		}

//		public void QueryReset()
//		{
//			this.query = null;
//		}

//		public bool Load(ReferLetterQuery query)
//		{
//			this.query = query;
//			base.InitQuery(this.query);
//			return this.Query.Load();
//		}
		
//		public ReferLetter AddNew()
//		{
//			ReferLetter entity = base.AddNewEntity() as ReferLetter;
			
//			return entity;
//		}

//		public ReferLetter FindByPrimaryKey(System.String regApptNo)
//		{
//			return base.FindByPrimaryKey(regApptNo) as ReferLetter;
//		}


//		#region IEnumerable<ReferLetter> Members

//		IEnumerator<ReferLetter> IEnumerable<ReferLetter>.GetEnumerator()
//		{
//			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
//			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

//			while(iterator.MoveNext())
//			{
//				yield return iterator.Current as ReferLetter;
//			}
//		}

//		#endregion
		
//		private ReferLetterQuery query;
//	}


//	/// <summary>
//	/// Encapsulates the 'ReferLetter' table
//	/// </summary>

//    [System.Diagnostics.DebuggerDisplay("ReferLetter ({RegApptNo})")]
//	[Serializable]
//	public partial class ReferLetter : esReferLetter
//	{
//		public ReferLetter()
//		{

//		}
	
//		public ReferLetter(DataRow row)
//			: base(row)
//		{

//		}
		
//		#region Housekeeping methods
//		override protected IMetadata Meta
//		{
//			get
//			{
//				return ReferLetterMetadata.Meta();
//			}
//		}
		
		
		
//		override protected esReferLetterQuery GetDynamicQuery()
//		{
//			if (this.query == null)
//			{
//				this.query = new ReferLetterQuery();
//				this.InitQuery(query);
//			}
//			return this.query;
//		}
//		#endregion
		



//		[BrowsableAttribute( false )]
//		public ReferLetterQuery Query
//		{
//			get
//			{
//				if (this.query == null)
//				{
//					this.query = new ReferLetterQuery();
//					base.InitQuery(this.query);
//				}

//				return this.query;
//			}
//		}

//		public void QueryReset()
//		{
//			this.query = null;
//		}
		

//		public bool Load(ReferLetterQuery query)
//		{
//			this.query = query;
//			base.InitQuery(this.query);
//			return this.Query.Load();
//		}
		
//		private ReferLetterQuery query;
//	}



//    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
//	[Serializable]
		
//	public partial class ReferLetterQuery : esReferLetterQuery
//	{
//		public ReferLetterQuery()
//		{

//		}		
		
//		public ReferLetterQuery(string joinAlias)
//		{
//			this.es.JoinAlias = joinAlias;
//		}	

//        override protected string GetQueryName()
//        {
//            return "ReferLetterQuery";
//        }
		
			
//	}


//	[Serializable]
//	public partial class ReferLetterMetadata : esMetadata, IMetadata
//	{
//		#region Protected Constructor
//		protected ReferLetterMetadata()
//		{
//			_columns = new esColumnMetadataCollection();
//			esColumnMetadata c;

//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.RegApptNo, 0, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.RegApptNo;
//			c.IsInPrimaryKey = true;
//			c.CharacterMaxLength = 20;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.FromRegistrationNo, 1, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.FromRegistrationNo;
//			c.CharacterMaxLength = 20;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.ActionExamTreatment, 2, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.ActionExamTreatment;
//			c.CharacterMaxLength = 500;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.Notes;
//			c.CharacterMaxLength = 500;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.Answer, 4, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.Answer;
//			c.CharacterMaxLength = 500;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.IsAppointment, 5, typeof(System.Boolean), esSystemType.Boolean);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.IsAppointment;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.LastUpdateDateTime;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//			c = new esColumnMetadata(ReferLetterMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
//			c.PropertyName = ReferLetterMetadata.PropertyNames.LastUpdateByUserID;
//			c.CharacterMaxLength = 15;
//			c.IsNullable = true;
//			_columns.Add(c);
				
//		}
//		#endregion	
	
//		static public ReferLetterMetadata Meta()
//		{
//			return meta;
//		}	
		
//		public Guid DataID
//		{
//			get { return base._dataID; }
//		}	
		
//		public bool MultiProviderMode
//		{
//			get { return false; }
//		}		

//		public esColumnMetadataCollection Columns
//		{
//			get	{ return base._columns; }
//		}
		
//		#region ColumnNames
//		public class ColumnNames
//		{ 
//			 public const string RegApptNo = "RegApptNo";
//			 public const string FromRegistrationNo = "FromRegistrationNo";
//			 public const string ActionExamTreatment = "ActionExamTreatment";
//			 public const string Notes = "Notes";
//			 public const string Answer = "Answer";
//			 public const string IsAppointment = "IsAppointment";
//			 public const string LastUpdateDateTime = "LastUpdateDateTime";
//			 public const string LastUpdateByUserID = "LastUpdateByUserID";
//		}
//		#endregion	
		
//		#region PropertyNames
//		public class PropertyNames
//		{ 
//			 public const string RegApptNo = "RegApptNo";
//			 public const string FromRegistrationNo = "FromRegistrationNo";
//			 public const string ActionExamTreatment = "ActionExamTreatment";
//			 public const string Notes = "Notes";
//			 public const string Answer = "Answer";
//			 public const string IsAppointment = "IsAppointment";
//			 public const string LastUpdateDateTime = "LastUpdateDateTime";
//			 public const string LastUpdateByUserID = "LastUpdateByUserID";
//		}
//		#endregion	

//		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
//		{
//			MapToMeta mapMethod = mapDelegates[mapName];

//			if (mapMethod != null)
//				return mapMethod(mapName);
//			else
//				return null;
//		}
		
//		#region MAP esDefault
		
//		static private int RegisterDelegateesDefault()
//		{
//			// This is only executed once per the life of the application
//			lock (typeof(ReferLetterMetadata))
//			{
//				if(ReferLetterMetadata.mapDelegates == null)
//				{
//					ReferLetterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
//				}
				
//				if (ReferLetterMetadata.meta == null)
//				{
//					ReferLetterMetadata.meta = new ReferLetterMetadata();
//				}
				
//				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
//				mapDelegates.Add("esDefault", mapMethod);
//				mapMethod("esDefault");
//			}
//			return 0;
//		}			

//		private esProviderSpecificMetadata esDefault(string mapName)
//		{
//			if(!_providerMetadataMaps.ContainsKey(mapName))
//			{
//				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				

//				meta.AddTypeMap("RegApptNo", new esTypeMap("varchar", "System.String"));
//				meta.AddTypeMap("FromRegistrationNo", new esTypeMap("varchar", "System.String"));
//				meta.AddTypeMap("ActionExamTreatment", new esTypeMap("varchar", "System.String"));
//				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
//				meta.AddTypeMap("Answer", new esTypeMap("varchar", "System.String"));
//				meta.AddTypeMap("IsAppointment", new esTypeMap("bit", "System.Boolean"));
//				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
//				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
//				meta.Source = "ReferLetter";
//				meta.Destination = "ReferLetter";
				
//				meta.spInsert = "proc_ReferLetterInsert";				
//				meta.spUpdate = "proc_ReferLetterUpdate";		
//				meta.spDelete = "proc_ReferLetterDelete";
//				meta.spLoadAll = "proc_ReferLetterLoadAll";
//				meta.spLoadByPrimaryKey = "proc_ReferLetterLoadByPrimaryKey";
				
//				this._providerMetadataMaps["esDefault"] = meta;
//			}
			
//			return this._providerMetadataMaps["esDefault"];
//		}

//		#endregion

//		static private ReferLetterMetadata meta;
//		static protected Dictionary<string, MapToMeta> mapDelegates;
//		static private int _esDefault = RegisterDelegateesDefault();
//	}
//}
