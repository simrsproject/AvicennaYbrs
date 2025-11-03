/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 6/16/2014 9:46:39 AM
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
	abstract public class esRegistrationQuestionFormCheckListCollection : esEntityCollection
	{
		public esRegistrationQuestionFormCheckListCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationQuestionFormCheckListCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationQuestionFormCheckListQuery query)
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
			this.InitQuery(query as esRegistrationQuestionFormCheckListQuery);
		}
		#endregion
		
		virtual public RegistrationQuestionFormCheckList DetachEntity(RegistrationQuestionFormCheckList entity)
		{
			return base.DetachEntity(entity) as RegistrationQuestionFormCheckList;
		}
		
		virtual public RegistrationQuestionFormCheckList AttachEntity(RegistrationQuestionFormCheckList entity)
		{
			return base.AttachEntity(entity) as RegistrationQuestionFormCheckList;
		}
		
		virtual public void Combine(RegistrationQuestionFormCheckListCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationQuestionFormCheckList this[int index]
		{
			get
			{
				return base[index] as RegistrationQuestionFormCheckList;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationQuestionFormCheckList);
		}
	}



	[Serializable]
	abstract public class esRegistrationQuestionFormCheckList : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationQuestionFormCheckListQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationQuestionFormCheckList()
		{

		}

		public esRegistrationQuestionFormCheckList(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String questionFormID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, questionFormID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, questionFormID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String questionFormID)
		{
			esRegistrationQuestionFormCheckListQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.QuestionFormID == questionFormID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("QuestionFormID",questionFormID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LasuUpdateByUserID": this.str.LasuUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to RegistrationQuestionFormCheckList.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				if(base.SetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.RegistrationNo, value))
				{
					this._UpToRegistrationByRegistrationNo = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to RegistrationQuestionFormCheckList.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				if(base.SetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.QuestionFormID, value))
				{
					this._UpToQuestionFormByQuestionFormID = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to RegistrationQuestionFormCheckList.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationQuestionFormCheckListMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationQuestionFormCheckListMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationQuestionFormCheckList.LasuUpdateByUserID
		/// </summary>
		virtual public System.String LasuUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.LasuUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationQuestionFormCheckListMetadata.ColumnNames.LasuUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected QuestionForm _UpToQuestionFormByQuestionFormID;
		[CLSCompliant(false)]
		internal protected Registration _UpToRegistrationByRegistrationNo;
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
			public esStrings(esRegistrationQuestionFormCheckList entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
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
				
			public System.String LasuUpdateByUserID
			{
				get
				{
					System.String data = entity.LasuUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LasuUpdateByUserID = null;
					else entity.LasuUpdateByUserID = Convert.ToString(value);
				}
			}
			

			private esRegistrationQuestionFormCheckList entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationQuestionFormCheckListQuery query)
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
				throw new Exception("esRegistrationQuestionFormCheckList can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationQuestionFormCheckList : esRegistrationQuestionFormCheckList
	{

				
		#region UpToQuestionFormByQuestionFormID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_RegistrationFormCheckList_QuestionForm
		/// </summary>

		[XmlIgnore]
		public QuestionForm UpToQuestionFormByQuestionFormID
		{
			get
			{
				if(this._UpToQuestionFormByQuestionFormID == null
					&& QuestionFormID != null					)
				{
					this._UpToQuestionFormByQuestionFormID = new QuestionForm();
					this._UpToQuestionFormByQuestionFormID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToQuestionFormByQuestionFormID", this._UpToQuestionFormByQuestionFormID);
					this._UpToQuestionFormByQuestionFormID.Query.Where(this._UpToQuestionFormByQuestionFormID.Query.QuestionFormID == this.QuestionFormID);
					this._UpToQuestionFormByQuestionFormID.Query.Load();
				}

				return this._UpToQuestionFormByQuestionFormID;
			}
			
			set
			{
				this.RemovePreSave("UpToQuestionFormByQuestionFormID");
				

				if(value == null)
				{
					this.QuestionFormID = null;
					this._UpToQuestionFormByQuestionFormID = null;
				}
				else
				{
					this.QuestionFormID = value.QuestionFormID;
					this._UpToQuestionFormByQuestionFormID = value;
					this.SetPreSave("UpToQuestionFormByQuestionFormID", this._UpToQuestionFormByQuestionFormID);
				}
				
			}
		}
		#endregion
		

				
		#region UpToRegistrationByRegistrationNo - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_RegistrationFormCheckList_Registration
		/// </summary>

		[XmlIgnore]
		public Registration UpToRegistrationByRegistrationNo
		{
			get
			{
				if(this._UpToRegistrationByRegistrationNo == null
					&& RegistrationNo != null					)
				{
					this._UpToRegistrationByRegistrationNo = new Registration();
					this._UpToRegistrationByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Where(this._UpToRegistrationByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Load();
				}

				return this._UpToRegistrationByRegistrationNo;
			}
			
			set
			{
				this.RemovePreSave("UpToRegistrationByRegistrationNo");
				

				if(value == null)
				{
					this.RegistrationNo = null;
					this._UpToRegistrationByRegistrationNo = null;
				}
				else
				{
					this.RegistrationNo = value.RegistrationNo;
					this._UpToRegistrationByRegistrationNo = value;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esRegistrationQuestionFormCheckListQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationQuestionFormCheckListMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationQuestionFormCheckListMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, RegistrationQuestionFormCheckListMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationQuestionFormCheckListMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LasuUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationQuestionFormCheckListMetadata.ColumnNames.LasuUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationQuestionFormCheckListCollection")]
	public partial class RegistrationQuestionFormCheckListCollection : esRegistrationQuestionFormCheckListCollection, IEnumerable<RegistrationQuestionFormCheckList>
	{
		public RegistrationQuestionFormCheckListCollection()
		{

		}
		
		public static implicit operator List<RegistrationQuestionFormCheckList>(RegistrationQuestionFormCheckListCollection coll)
		{
			List<RegistrationQuestionFormCheckList> list = new List<RegistrationQuestionFormCheckList>();
			
			foreach (RegistrationQuestionFormCheckList emp in coll)
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
				return  RegistrationQuestionFormCheckListMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuestionFormCheckListQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationQuestionFormCheckList(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationQuestionFormCheckList();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationQuestionFormCheckListQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuestionFormCheckListQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationQuestionFormCheckListQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationQuestionFormCheckList AddNew()
		{
			RegistrationQuestionFormCheckList entity = base.AddNewEntity() as RegistrationQuestionFormCheckList;
			
			return entity;
		}

		public RegistrationQuestionFormCheckList FindByPrimaryKey(System.String registrationNo, System.String questionFormID)
		{
			return base.FindByPrimaryKey(registrationNo, questionFormID) as RegistrationQuestionFormCheckList;
		}


		#region IEnumerable<RegistrationQuestionFormCheckList> Members

		IEnumerator<RegistrationQuestionFormCheckList> IEnumerable<RegistrationQuestionFormCheckList>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationQuestionFormCheckList;
			}
		}

		#endregion
		
		private RegistrationQuestionFormCheckListQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationQuestionFormCheckList' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationQuestionFormCheckList ({RegistrationNo},{QuestionFormID})")]
	[Serializable]
	public partial class RegistrationQuestionFormCheckList : esRegistrationQuestionFormCheckList
	{
		public RegistrationQuestionFormCheckList()
		{

		}
	
		public RegistrationQuestionFormCheckList(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationQuestionFormCheckListMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationQuestionFormCheckListQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuestionFormCheckListQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationQuestionFormCheckListQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuestionFormCheckListQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationQuestionFormCheckListQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationQuestionFormCheckListQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationQuestionFormCheckListQuery : esRegistrationQuestionFormCheckListQuery
	{
		public RegistrationQuestionFormCheckListQuery()
		{

		}		
		
		public RegistrationQuestionFormCheckListQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationQuestionFormCheckListQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationQuestionFormCheckListMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationQuestionFormCheckListMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationQuestionFormCheckListMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationQuestionFormCheckListMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationQuestionFormCheckListMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationQuestionFormCheckListMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationQuestionFormCheckListMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationQuestionFormCheckListMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationQuestionFormCheckListMetadata.ColumnNames.LasuUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationQuestionFormCheckListMetadata.PropertyNames.LasuUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationQuestionFormCheckListMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string QuestionFormID = "QuestionFormID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LasuUpdateByUserID = "LasuUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string QuestionFormID = "QuestionFormID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LasuUpdateByUserID = "LasuUpdateByUserID";
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
			lock (typeof(RegistrationQuestionFormCheckListMetadata))
			{
				if(RegistrationQuestionFormCheckListMetadata.mapDelegates == null)
				{
					RegistrationQuestionFormCheckListMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationQuestionFormCheckListMetadata.meta == null)
				{
					RegistrationQuestionFormCheckListMetadata.meta = new RegistrationQuestionFormCheckListMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LasuUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationQuestionFormCheckList";
				meta.Destination = "RegistrationQuestionFormCheckList";
				
				meta.spInsert = "proc_RegistrationQuestionFormCheckListInsert";				
				meta.spUpdate = "proc_RegistrationQuestionFormCheckListUpdate";		
				meta.spDelete = "proc_RegistrationQuestionFormCheckListDelete";
				meta.spLoadAll = "proc_RegistrationQuestionFormCheckListLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationQuestionFormCheckListLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationQuestionFormCheckListMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
