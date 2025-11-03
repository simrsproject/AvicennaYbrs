/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 4/3/2015 2:36:34 PM
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
	abstract public class esRegistrationInfoMedicalCollection : esEntityCollection
	{
		public esRegistrationInfoMedicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationInfoMedicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicalQuery query)
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
			this.InitQuery(query as esRegistrationInfoMedicalQuery);
		}
		#endregion
		
		virtual public RegistrationInfoMedical DetachEntity(RegistrationInfoMedical entity)
		{
			return base.DetachEntity(entity) as RegistrationInfoMedical;
		}
		
		virtual public RegistrationInfoMedical AttachEntity(RegistrationInfoMedical entity)
		{
			return base.AttachEntity(entity) as RegistrationInfoMedical;
		}
		
		virtual public void Combine(RegistrationInfoMedicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationInfoMedical this[int index]
		{
			get
			{
				return base[index] as RegistrationInfoMedical;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationInfoMedical);
		}
	}



	[Serializable]
	abstract public class esRegistrationInfoMedical : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationInfoMedicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationInfoMedical()
		{

		}

		public esRegistrationInfoMedical(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 id)
		{
			esRegistrationInfoMedicalQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 id)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "SRMedicalNotesInputType": this.str.SRMedicalNotesInputType = (string)value; break;							
						case "DateTimeInfo": this.str.DateTimeInfo = (string)value; break;							
						case "Info1": this.str.Info1 = (string)value; break;							
						case "Info2": this.str.Info2 = (string)value; break;							
						case "Info3": this.str.Info3 = (string)value; break;							
						case "Info4": this.str.Info4 = (string)value; break;							
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int64)
								this.Id = (System.Int64?)value;
							break;
						
						case "DateTimeInfo":
						
							if (value == null || value is System.DateTime)
								this.DateTimeInfo = (System.DateTime?)value;
							break;
						
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationInfoMedical.ID
		/// </summary>
		virtual public System.Int64? Id
		{
			get
			{
				return base.GetSystemInt64(RegistrationInfoMedicalMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt64(RegistrationInfoMedicalMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.SRMedicalNotesInputType
		/// </summary>
		virtual public System.String SRMedicalNotesInputType
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.SRMedicalNotesInputType);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.SRMedicalNotesInputType, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.DateTimeInfo
		/// </summary>
		virtual public System.DateTime? DateTimeInfo
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.DateTimeInfo);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.DateTimeInfo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.Info1
		/// </summary>
		virtual public System.String Info1
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info1);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info1, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.Info2
		/// </summary>
		virtual public System.String Info2
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info2);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info2, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.Info3
		/// </summary>
		virtual public System.String Info3
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info3);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info3, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.Info4
		/// </summary>
		virtual public System.String Info4
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info4);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.Info4, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedical.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationInfoMedical entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int64? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt64(value);
				}
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
				
			public System.String SRMedicalNotesInputType
			{
				get
				{
					System.String data = entity.SRMedicalNotesInputType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalNotesInputType = null;
					else entity.SRMedicalNotesInputType = Convert.ToString(value);
				}
			}
				
			public System.String DateTimeInfo
			{
				get
				{
					System.DateTime? data = entity.DateTimeInfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateTimeInfo = null;
					else entity.DateTimeInfo = Convert.ToDateTime(value);
				}
			}
				
			public System.String Info1
			{
				get
				{
					System.String data = entity.Info1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info1 = null;
					else entity.Info1 = Convert.ToString(value);
				}
			}
				
			public System.String Info2
			{
				get
				{
					System.String data = entity.Info2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info2 = null;
					else entity.Info2 = Convert.ToString(value);
				}
			}
				
			public System.String Info3
			{
				get
				{
					System.String data = entity.Info3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info3 = null;
					else entity.Info3 = Convert.ToString(value);
				}
			}
				
			public System.String Info4
			{
				get
				{
					System.String data = entity.Info4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info4 = null;
					else entity.Info4 = Convert.ToString(value);
				}
			}
				
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			

			private esRegistrationInfoMedical entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicalQuery query)
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
				throw new Exception("esRegistrationInfoMedical can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esRegistrationInfoMedicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicalMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.Id, esSystemType.Int64);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMedicalNotesInputType
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.SRMedicalNotesInputType, esSystemType.String);
			}
		} 
		
		public esQueryItem DateTimeInfo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.DateTimeInfo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Info1
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.Info1, esSystemType.String);
			}
		} 
		
		public esQueryItem Info2
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.Info2, esSystemType.String);
			}
		} 
		
		public esQueryItem Info3
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.Info3, esSystemType.String);
			}
		} 
		
		public esQueryItem Info4
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.Info4, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoMedicalCollection")]
	public partial class RegistrationInfoMedicalCollection : esRegistrationInfoMedicalCollection, IEnumerable<RegistrationInfoMedical>
	{
		public RegistrationInfoMedicalCollection()
		{

		}
		
		public static implicit operator List<RegistrationInfoMedical>(RegistrationInfoMedicalCollection coll)
		{
			List<RegistrationInfoMedical> list = new List<RegistrationInfoMedical>();
			
			foreach (RegistrationInfoMedical emp in coll)
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
				return  RegistrationInfoMedicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationInfoMedical(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationInfoMedical();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationInfoMedicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationInfoMedicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationInfoMedical AddNew()
		{
			RegistrationInfoMedical entity = base.AddNewEntity() as RegistrationInfoMedical;
			
			return entity;
		}

		public RegistrationInfoMedical FindByPrimaryKey(System.Int64 id)
		{
			return base.FindByPrimaryKey(id) as RegistrationInfoMedical;
		}


		#region IEnumerable<RegistrationInfoMedical> Members

		IEnumerator<RegistrationInfoMedical> IEnumerable<RegistrationInfoMedical>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationInfoMedical;
			}
		}

		#endregion
		
		private RegistrationInfoMedicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationInfoMedical' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationInfoMedical ({Id})")]
	[Serializable]
	public partial class RegistrationInfoMedical : esRegistrationInfoMedical
	{
		public RegistrationInfoMedical()
		{

		}
	
		public RegistrationInfoMedical(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicalMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationInfoMedicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationInfoMedicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationInfoMedicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationInfoMedicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationInfoMedicalQuery : esRegistrationInfoMedicalQuery
	{
		public RegistrationInfoMedicalQuery()
		{

		}		
		
		public RegistrationInfoMedicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationInfoMedicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationInfoMedicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationInfoMedicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.Id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.SRMedicalNotesInputType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.SRMedicalNotesInputType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.DateTimeInfo, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.DateTimeInfo;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.Info1, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.Info1;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.Info2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.Info2;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.Info3, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.Info3;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.Info4, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.Info4;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicalMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicalMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationInfoMedicalMetadata Meta()
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
			 public const string Id = "ID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRMedicalNotesInputType = "SRMedicalNotesInputType";
			 public const string DateTimeInfo = "DateTimeInfo";
			 public const string Info1 = "Info1";
			 public const string Info2 = "Info2";
			 public const string Info3 = "Info3";
			 public const string Info4 = "Info4";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRMedicalNotesInputType = "SRMedicalNotesInputType";
			 public const string DateTimeInfo = "DateTimeInfo";
			 public const string Info1 = "Info1";
			 public const string Info2 = "Info2";
			 public const string Info3 = "Info3";
			 public const string Info4 = "Info4";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationInfoMedicalMetadata))
			{
				if(RegistrationInfoMedicalMetadata.mapDelegates == null)
				{
					RegistrationInfoMedicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationInfoMedicalMetadata.meta == null)
				{
					RegistrationInfoMedicalMetadata.meta = new RegistrationInfoMedicalMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalNotesInputType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateTimeInfo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Info1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "RegistrationInfoMedical";
				meta.Destination = "RegistrationInfoMedical";
				
				meta.spInsert = "proc_RegistrationInfoMedicalInsert";				
				meta.spUpdate = "proc_RegistrationInfoMedicalUpdate";		
				meta.spDelete = "proc_RegistrationInfoMedicalDelete";
				meta.spLoadAll = "proc_RegistrationInfoMedicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationInfoMedicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationInfoMedicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
