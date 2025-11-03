/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:20 PM
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
	abstract public class esOperationalTimeCollection : esEntityCollectionWAuditLog
	{
		public esOperationalTimeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OperationalTimeCollection";
		}

		#region Query Logic
		protected void InitQuery(esOperationalTimeQuery query)
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
			this.InitQuery(query as esOperationalTimeQuery);
		}
		#endregion
		
		virtual public OperationalTime DetachEntity(OperationalTime entity)
		{
			return base.DetachEntity(entity) as OperationalTime;
		}
		
		virtual public OperationalTime AttachEntity(OperationalTime entity)
		{
			return base.AttachEntity(entity) as OperationalTime;
		}
		
		virtual public void Combine(OperationalTimeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OperationalTime this[int index]
		{
			get
			{
				return base[index] as OperationalTime;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OperationalTime);
		}
	}



	[Serializable]
	abstract public class esOperationalTime : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOperationalTimeQuery GetDynamicQuery()
		{
			return null;
		}

		public esOperationalTime()
		{

		}

		public esOperationalTime(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String operationalTimeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(operationalTimeID);
			else
				return LoadByPrimaryKeyStoredProcedure(operationalTimeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String operationalTimeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(operationalTimeID);
			else
				return LoadByPrimaryKeyStoredProcedure(operationalTimeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String operationalTimeID)
		{
			esOperationalTimeQuery query = this.GetDynamicQuery();
			query.Where(query.OperationalTimeID == operationalTimeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String operationalTimeID)
		{
			esParameters parms = new esParameters();
			parms.Add("OperationalTimeID",operationalTimeID);
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
						case "OperationalTimeID": this.str.OperationalTimeID = (string)value; break;							
						case "OperationalTimeName": this.str.OperationalTimeName = (string)value; break;							
						case "StartTime1": this.str.StartTime1 = (string)value; break;							
						case "EndTime1": this.str.EndTime1 = (string)value; break;							
						case "StartTime2": this.str.StartTime2 = (string)value; break;							
						case "EndTime2": this.str.EndTime2 = (string)value; break;							
						case "StartTime3": this.str.StartTime3 = (string)value; break;							
						case "EndTime3": this.str.EndTime3 = (string)value; break;							
						case "StartTime4": this.str.StartTime4 = (string)value; break;							
						case "EndTime4": this.str.EndTime4 = (string)value; break;							
						case "StartTime5": this.str.StartTime5 = (string)value; break;							
						case "EndTime5": this.str.EndTime5 = (string)value; break;							
						case "OperationalTimeBackcolor": this.str.OperationalTimeBackcolor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to OperationalTime.OperationalTimeID
		/// </summary>
		virtual public System.String OperationalTimeID
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeID);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeID, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.OperationalTimeName
		/// </summary>
		virtual public System.String OperationalTimeName
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeName);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeName, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.StartTime1
		/// </summary>
		virtual public System.String StartTime1
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.StartTime1);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.StartTime1, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.EndTime1
		/// </summary>
		virtual public System.String EndTime1
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.EndTime1);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.EndTime1, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.StartTime2
		/// </summary>
		virtual public System.String StartTime2
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.StartTime2);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.StartTime2, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.EndTime2
		/// </summary>
		virtual public System.String EndTime2
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.EndTime2);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.EndTime2, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.StartTime3
		/// </summary>
		virtual public System.String StartTime3
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.StartTime3);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.StartTime3, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.EndTime3
		/// </summary>
		virtual public System.String EndTime3
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.EndTime3);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.EndTime3, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.StartTime4
		/// </summary>
		virtual public System.String StartTime4
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.StartTime4);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.StartTime4, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.EndTime4
		/// </summary>
		virtual public System.String EndTime4
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.EndTime4);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.EndTime4, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.StartTime5
		/// </summary>
		virtual public System.String StartTime5
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.StartTime5);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.StartTime5, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.EndTime5
		/// </summary>
		virtual public System.String EndTime5
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.EndTime5);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.EndTime5, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.OperationalTimeBackcolor
		/// </summary>
		virtual public System.String OperationalTimeBackcolor
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeBackcolor);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.OperationalTimeBackcolor, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(OperationalTimeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(OperationalTimeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationalTime.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(OperationalTimeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(OperationalTimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esOperationalTime entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OperationalTimeID
			{
				get
				{
					System.String data = entity.OperationalTimeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeID = null;
					else entity.OperationalTimeID = Convert.ToString(value);
				}
			}
				
			public System.String OperationalTimeName
			{
				get
				{
					System.String data = entity.OperationalTimeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeName = null;
					else entity.OperationalTimeName = Convert.ToString(value);
				}
			}
				
			public System.String StartTime1
			{
				get
				{
					System.String data = entity.StartTime1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime1 = null;
					else entity.StartTime1 = Convert.ToString(value);
				}
			}
				
			public System.String EndTime1
			{
				get
				{
					System.String data = entity.EndTime1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime1 = null;
					else entity.EndTime1 = Convert.ToString(value);
				}
			}
				
			public System.String StartTime2
			{
				get
				{
					System.String data = entity.StartTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime2 = null;
					else entity.StartTime2 = Convert.ToString(value);
				}
			}
				
			public System.String EndTime2
			{
				get
				{
					System.String data = entity.EndTime2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime2 = null;
					else entity.EndTime2 = Convert.ToString(value);
				}
			}
				
			public System.String StartTime3
			{
				get
				{
					System.String data = entity.StartTime3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime3 = null;
					else entity.StartTime3 = Convert.ToString(value);
				}
			}
				
			public System.String EndTime3
			{
				get
				{
					System.String data = entity.EndTime3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime3 = null;
					else entity.EndTime3 = Convert.ToString(value);
				}
			}
				
			public System.String StartTime4
			{
				get
				{
					System.String data = entity.StartTime4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime4 = null;
					else entity.StartTime4 = Convert.ToString(value);
				}
			}
				
			public System.String EndTime4
			{
				get
				{
					System.String data = entity.EndTime4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime4 = null;
					else entity.EndTime4 = Convert.ToString(value);
				}
			}
				
			public System.String StartTime5
			{
				get
				{
					System.String data = entity.StartTime5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime5 = null;
					else entity.StartTime5 = Convert.ToString(value);
				}
			}
				
			public System.String EndTime5
			{
				get
				{
					System.String data = entity.EndTime5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime5 = null;
					else entity.EndTime5 = Convert.ToString(value);
				}
			}
				
			public System.String OperationalTimeBackcolor
			{
				get
				{
					System.String data = entity.OperationalTimeBackcolor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeBackcolor = null;
					else entity.OperationalTimeBackcolor = Convert.ToString(value);
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
			

			private esOperationalTime entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOperationalTimeQuery query)
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
				throw new Exception("esOperationalTime can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class OperationalTime : esOperationalTime
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
	abstract public class esOperationalTimeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OperationalTimeMetadata.Meta();
			}
		}	
		

		public esQueryItem OperationalTimeID
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.OperationalTimeID, esSystemType.String);
			}
		} 
		
		public esQueryItem OperationalTimeName
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.OperationalTimeName, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime1
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.StartTime1, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime1
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.EndTime1, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime2
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.StartTime2, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime2
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.EndTime2, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime3
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.StartTime3, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime3
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.EndTime3, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime4
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.StartTime4, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime4
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.EndTime4, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime5
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.StartTime5, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime5
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.EndTime5, esSystemType.String);
			}
		} 
		
		public esQueryItem OperationalTimeBackcolor
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.OperationalTimeBackcolor, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, OperationalTimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OperationalTimeCollection")]
	public partial class OperationalTimeCollection : esOperationalTimeCollection, IEnumerable<OperationalTime>
	{
		public OperationalTimeCollection()
		{

		}
		
		public static implicit operator List<OperationalTime>(OperationalTimeCollection coll)
		{
			List<OperationalTime> list = new List<OperationalTime>();
			
			foreach (OperationalTime emp in coll)
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
				return  OperationalTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationalTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OperationalTime(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OperationalTime();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OperationalTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationalTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OperationalTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OperationalTime AddNew()
		{
			OperationalTime entity = base.AddNewEntity() as OperationalTime;
			
			return entity;
		}

		public OperationalTime FindByPrimaryKey(System.String operationalTimeID)
		{
			return base.FindByPrimaryKey(operationalTimeID) as OperationalTime;
		}


		#region IEnumerable<OperationalTime> Members

		IEnumerator<OperationalTime> IEnumerable<OperationalTime>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OperationalTime;
			}
		}

		#endregion
		
		private OperationalTimeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OperationalTime' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OperationalTime ({OperationalTimeID})")]
	[Serializable]
	public partial class OperationalTime : esOperationalTime
	{
		public OperationalTime()
		{

		}
	
		public OperationalTime(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OperationalTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esOperationalTimeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationalTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OperationalTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationalTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OperationalTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OperationalTimeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OperationalTimeQuery : esOperationalTimeQuery
	{
		public OperationalTimeQuery()
		{

		}		
		
		public OperationalTimeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OperationalTimeQuery";
        }
		
			
	}


	[Serializable]
	public partial class OperationalTimeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OperationalTimeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.OperationalTimeID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.OperationalTimeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.OperationalTimeName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.OperationalTimeName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.StartTime1, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.StartTime1;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.EndTime1, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.EndTime1;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.StartTime2, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.StartTime2;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.EndTime2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.EndTime2;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.StartTime3, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.StartTime3;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.EndTime3, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.EndTime3;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.StartTime4, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.StartTime4;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.EndTime4, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.EndTime4;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.StartTime5, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.StartTime5;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.EndTime5, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.EndTime5;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.OperationalTimeBackcolor, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.OperationalTimeBackcolor;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationalTimeMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationalTimeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OperationalTimeMetadata Meta()
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
			 public const string OperationalTimeID = "OperationalTimeID";
			 public const string OperationalTimeName = "OperationalTimeName";
			 public const string StartTime1 = "StartTime1";
			 public const string EndTime1 = "EndTime1";
			 public const string StartTime2 = "StartTime2";
			 public const string EndTime2 = "EndTime2";
			 public const string StartTime3 = "StartTime3";
			 public const string EndTime3 = "EndTime3";
			 public const string StartTime4 = "StartTime4";
			 public const string EndTime4 = "EndTime4";
			 public const string StartTime5 = "StartTime5";
			 public const string EndTime5 = "EndTime5";
			 public const string OperationalTimeBackcolor = "OperationalTimeBackcolor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OperationalTimeID = "OperationalTimeID";
			 public const string OperationalTimeName = "OperationalTimeName";
			 public const string StartTime1 = "StartTime1";
			 public const string EndTime1 = "EndTime1";
			 public const string StartTime2 = "StartTime2";
			 public const string EndTime2 = "EndTime2";
			 public const string StartTime3 = "StartTime3";
			 public const string EndTime3 = "EndTime3";
			 public const string StartTime4 = "StartTime4";
			 public const string EndTime4 = "EndTime4";
			 public const string StartTime5 = "StartTime5";
			 public const string EndTime5 = "EndTime5";
			 public const string OperationalTimeBackcolor = "OperationalTimeBackcolor";
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
			lock (typeof(OperationalTimeMetadata))
			{
				if(OperationalTimeMetadata.mapDelegates == null)
				{
					OperationalTimeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OperationalTimeMetadata.meta == null)
				{
					OperationalTimeMetadata.meta = new OperationalTimeMetadata();
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
				

				meta.AddTypeMap("OperationalTimeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperationalTimeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartTime1", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndTime1", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("StartTime2", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndTime2", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("StartTime3", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndTime3", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("StartTime4", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndTime4", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("StartTime5", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndTime5", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("OperationalTimeBackcolor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "OperationalTime";
				meta.Destination = "OperationalTime";
				
				meta.spInsert = "proc_OperationalTimeInsert";				
				meta.spUpdate = "proc_OperationalTimeUpdate";		
				meta.spDelete = "proc_OperationalTimeDelete";
				meta.spLoadAll = "proc_OperationalTimeLoadAll";
				meta.spLoadByPrimaryKey = "proc_OperationalTimeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OperationalTimeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
