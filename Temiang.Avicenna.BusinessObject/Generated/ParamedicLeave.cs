/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 8/16/2014 2:20:43 PM
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
    abstract public class esParamedicLeaveCollection : esEntityCollectionWAuditLog
	{
		public esParamedicLeaveCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicLeaveCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicLeaveQuery query)
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
			this.InitQuery(query as esParamedicLeaveQuery);
		}
		#endregion
		
		virtual public ParamedicLeave DetachEntity(ParamedicLeave entity)
		{
			return base.DetachEntity(entity) as ParamedicLeave;
		}
		
		virtual public ParamedicLeave AttachEntity(ParamedicLeave entity)
		{
			return base.AttachEntity(entity) as ParamedicLeave;
		}
		
		virtual public void Combine(ParamedicLeaveCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicLeave this[int index]
		{
			get
			{
				return base[index] as ParamedicLeave;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicLeave);
		}
	}



	[Serializable]
	abstract public class esParamedicLeave : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicLeaveQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicLeave()
		{

		}

		public esParamedicLeave(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esParamedicLeaveQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "SRPhysicianLeaveReason": this.str.SRPhysicianLeaveReason = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "SubsParamedicIP": this.str.SubsParamedicIP = (string)value; break;							
						case "SubsParamedicOP": this.str.SubsParamedicOP = (string)value; break;							
						case "SubsParamedicEMR": this.str.SubsParamedicEMR = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
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
		/// Maps to ParamedicLeave.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.SRPhysicianLeaveReason
		/// </summary>
		virtual public System.String SRPhysicianLeaveReason
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.SRPhysicianLeaveReason);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.SRPhysicianLeaveReason, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicLeaveMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicLeaveMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicLeaveMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.SubsParamedicIP
		/// </summary>
		virtual public System.String SubsParamedicIP
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicIP);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicIP, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.SubsParamedicOP
		/// </summary>
		virtual public System.String SubsParamedicOP
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicOP);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicOP, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicLeave.SubsParamedicEMR
		/// </summary>
		virtual public System.String SubsParamedicEMR
		{
			get
			{
				return base.GetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicEMR);
			}
			
			set
			{
				base.SetSystemString(ParamedicLeaveMetadata.ColumnNames.SubsParamedicEMR, value);
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
			public esStrings(esParamedicLeave entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
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
				
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SRPhysicianLeaveReason
			{
				get
				{
					System.String data = entity.SRPhysicianLeaveReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianLeaveReason = null;
					else entity.SRPhysicianLeaveReason = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
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
				
			public System.String SubsParamedicIP
			{
				get
				{
					System.String data = entity.SubsParamedicIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubsParamedicIP = null;
					else entity.SubsParamedicIP = Convert.ToString(value);
				}
			}
				
			public System.String SubsParamedicOP
			{
				get
				{
					System.String data = entity.SubsParamedicOP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubsParamedicOP = null;
					else entity.SubsParamedicOP = Convert.ToString(value);
				}
			}
				
			public System.String SubsParamedicEMR
			{
				get
				{
					System.String data = entity.SubsParamedicEMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubsParamedicEMR = null;
					else entity.SubsParamedicEMR = Convert.ToString(value);
				}
			}
			

			private esParamedicLeave entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicLeaveQuery query)
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
				throw new Exception("esParamedicLeave can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicLeaveQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicLeaveMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRPhysicianLeaveReason
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.SRPhysicianLeaveReason, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SubsParamedicIP
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.SubsParamedicIP, esSystemType.String);
			}
		} 
		
		public esQueryItem SubsParamedicOP
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.SubsParamedicOP, esSystemType.String);
			}
		} 
		
		public esQueryItem SubsParamedicEMR
		{
			get
			{
				return new esQueryItem(this, ParamedicLeaveMetadata.ColumnNames.SubsParamedicEMR, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicLeaveCollection")]
	public partial class ParamedicLeaveCollection : esParamedicLeaveCollection, IEnumerable<ParamedicLeave>
	{
		public ParamedicLeaveCollection()
		{

		}
		
		public static implicit operator List<ParamedicLeave>(ParamedicLeaveCollection coll)
		{
			List<ParamedicLeave> list = new List<ParamedicLeave>();
			
			foreach (ParamedicLeave emp in coll)
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
				return  ParamedicLeaveMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicLeaveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicLeave(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicLeave();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicLeaveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicLeaveQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicLeaveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicLeave AddNew()
		{
			ParamedicLeave entity = base.AddNewEntity() as ParamedicLeave;
			
			return entity;
		}

		public ParamedicLeave FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as ParamedicLeave;
		}


		#region IEnumerable<ParamedicLeave> Members

		IEnumerator<ParamedicLeave> IEnumerable<ParamedicLeave>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicLeave;
			}
		}

		#endregion
		
		private ParamedicLeaveQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicLeave' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicLeave ({TransactionNo})")]
	[Serializable]
	public partial class ParamedicLeave : esParamedicLeave
	{
		public ParamedicLeave()
		{

		}
	
		public ParamedicLeave(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicLeaveMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicLeaveQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicLeaveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicLeaveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicLeaveQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicLeaveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicLeaveQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicLeaveQuery : esParamedicLeaveQuery
	{
		public ParamedicLeaveQuery()
		{

		}		
		
		public ParamedicLeaveQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicLeaveQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicLeaveMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicLeaveMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.StartDate;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.EndDate;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.SRPhysicianLeaveReason, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.SRPhysicianLeaveReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.SubsParamedicIP, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.SubsParamedicIP;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.SubsParamedicOP, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.SubsParamedicOP;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicLeaveMetadata.ColumnNames.SubsParamedicEMR, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicLeaveMetadata.PropertyNames.SubsParamedicEMR;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicLeaveMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string SRPhysicianLeaveReason = "SRPhysicianLeaveReason";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string SubsParamedicIP = "SubsParamedicIP";
			 public const string SubsParamedicOP = "SubsParamedicOP";
			 public const string SubsParamedicEMR = "SubsParamedicEMR";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string ParamedicID = "ParamedicID";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string SRPhysicianLeaveReason = "SRPhysicianLeaveReason";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string SubsParamedicIP = "SubsParamedicIP";
			 public const string SubsParamedicOP = "SubsParamedicOP";
			 public const string SubsParamedicEMR = "SubsParamedicEMR";
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
			lock (typeof(ParamedicLeaveMetadata))
			{
				if(ParamedicLeaveMetadata.mapDelegates == null)
				{
					ParamedicLeaveMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicLeaveMetadata.meta == null)
				{
					ParamedicLeaveMetadata.meta = new ParamedicLeaveMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("SRPhysicianLeaveReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SubsParamedicIP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubsParamedicOP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubsParamedicEMR", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicLeave";
				meta.Destination = "ParamedicLeave";
				
				meta.spInsert = "proc_ParamedicLeaveInsert";				
				meta.spUpdate = "proc_ParamedicLeaveUpdate";		
				meta.spDelete = "proc_ParamedicLeaveDelete";
				meta.spLoadAll = "proc_ParamedicLeaveLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicLeaveLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicLeaveMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
