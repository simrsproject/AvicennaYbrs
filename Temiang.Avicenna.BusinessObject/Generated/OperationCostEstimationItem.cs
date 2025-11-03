/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/10/2015 2:28:41 PM
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
	abstract public class esOperationCostEstimationItemCollection : esEntityCollectionWAuditLog
	{
		public esOperationCostEstimationItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OperationCostEstimationItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esOperationCostEstimationItemQuery query)
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
			this.InitQuery(query as esOperationCostEstimationItemQuery);
		}
		#endregion
		
		virtual public OperationCostEstimationItem DetachEntity(OperationCostEstimationItem entity)
		{
			return base.DetachEntity(entity) as OperationCostEstimationItem;
		}
		
		virtual public OperationCostEstimationItem AttachEntity(OperationCostEstimationItem entity)
		{
			return base.AttachEntity(entity) as OperationCostEstimationItem;
		}
		
		virtual public void Combine(OperationCostEstimationItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OperationCostEstimationItem this[int index]
		{
			get
			{
				return base[index] as OperationCostEstimationItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OperationCostEstimationItem);
		}
	}



	[Serializable]
	abstract public class esOperationCostEstimationItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOperationCostEstimationItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esOperationCostEstimationItem()
		{

		}

		public esOperationCostEstimationItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo, System.String itemGroupID, System.String sRBillingGroup)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo, itemGroupID, sRBillingGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo, itemGroupID, sRBillingGroup);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo, System.String itemGroupID, System.String sRBillingGroup)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo, itemGroupID, sRBillingGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo, itemGroupID, sRBillingGroup);
		}

		private bool LoadByPrimaryKeyDynamic(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo, System.String itemGroupID, System.String sRBillingGroup)
		{
			esOperationCostEstimationItemQuery query = this.GetDynamicQuery();
			query.Where(query.DiagnoseID == diagnoseID, query.ProcedureID == procedureID, query.SRProcedureCategory == sRProcedureCategory, query.ClassID == classID, query.RegistrationNo == registrationNo, query.ItemGroupID == itemGroupID, query.SRBillingGroup == sRBillingGroup);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo, System.String itemGroupID, System.String sRBillingGroup)
		{
			esParameters parms = new esParameters();
			parms.Add("DiagnoseID",diagnoseID);			parms.Add("ProcedureID",procedureID);			parms.Add("SRProcedureCategory",sRProcedureCategory);			parms.Add("ClassID",classID);			parms.Add("RegistrationNo",registrationNo);			parms.Add("ItemGroupID",itemGroupID);			parms.Add("SRBillingGroup",sRBillingGroup);
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
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;							
						case "ProcedureID": this.str.ProcedureID = (string)value; break;							
						case "SRProcedureCategory": this.str.SRProcedureCategory = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;							
						case "ItemGroupName": this.str.ItemGroupName = (string)value; break;							
						case "SRBillingGroup": this.str.SRBillingGroup = (string)value; break;							
						case "CostAmount": this.str.CostAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CostAmount":
						
							if (value == null || value is System.Decimal)
								this.CostAmount = (System.Decimal?)value;
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
		/// Maps to OperationCostEstimationItem.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.ProcedureID
		/// </summary>
		virtual public System.String ProcedureID
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ProcedureID);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ProcedureID, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.SRProcedureCategory
		/// </summary>
		virtual public System.String SRProcedureCategory
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.SRProcedureCategory);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.SRProcedureCategory, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.ItemGroupName
		/// </summary>
		virtual public System.String ItemGroupName
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupName);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupName, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.SRBillingGroup
		/// </summary>
		virtual public System.String SRBillingGroup
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.SRBillingGroup);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.SRBillingGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.CostAmount
		/// </summary>
		virtual public System.Decimal? CostAmount
		{
			get
			{
				return base.GetSystemDecimal(OperationCostEstimationItemMetadata.ColumnNames.CostAmount);
			}
			
			set
			{
				base.SetSystemDecimal(OperationCostEstimationItemMetadata.ColumnNames.CostAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to OperationCostEstimationItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esOperationCostEstimationItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
				
			public System.String ProcedureID
			{
				get
				{
					System.String data = entity.ProcedureID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureID = null;
					else entity.ProcedureID = Convert.ToString(value);
				}
			}
				
			public System.String SRProcedureCategory
			{
				get
				{
					System.String data = entity.SRProcedureCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedureCategory = null;
					else entity.SRProcedureCategory = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
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
				
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
				
			public System.String ItemGroupName
			{
				get
				{
					System.String data = entity.ItemGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupName = null;
					else entity.ItemGroupName = Convert.ToString(value);
				}
			}
				
			public System.String SRBillingGroup
			{
				get
				{
					System.String data = entity.SRBillingGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBillingGroup = null;
					else entity.SRBillingGroup = Convert.ToString(value);
				}
			}
				
			public System.String CostAmount
			{
				get
				{
					System.Decimal? data = entity.CostAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostAmount = null;
					else entity.CostAmount = Convert.ToDecimal(value);
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
			

			private esOperationCostEstimationItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOperationCostEstimationItemQuery query)
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
				throw new Exception("esOperationCostEstimationItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class OperationCostEstimationItem : esOperationCostEstimationItem
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
	abstract public class esOperationCostEstimationItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OperationCostEstimationItemMetadata.Meta();
			}
		}	
		

		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureID
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.ProcedureID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRProcedureCategory
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.SRProcedureCategory, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemGroupName
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.ItemGroupName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRBillingGroup
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.SRBillingGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem CostAmount
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.CostAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, OperationCostEstimationItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OperationCostEstimationItemCollection")]
	public partial class OperationCostEstimationItemCollection : esOperationCostEstimationItemCollection, IEnumerable<OperationCostEstimationItem>
	{
		public OperationCostEstimationItemCollection()
		{

		}
		
		public static implicit operator List<OperationCostEstimationItem>(OperationCostEstimationItemCollection coll)
		{
			List<OperationCostEstimationItem> list = new List<OperationCostEstimationItem>();
			
			foreach (OperationCostEstimationItem emp in coll)
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
				return  OperationCostEstimationItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationCostEstimationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OperationCostEstimationItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OperationCostEstimationItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OperationCostEstimationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationCostEstimationItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OperationCostEstimationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OperationCostEstimationItem AddNew()
		{
			OperationCostEstimationItem entity = base.AddNewEntity() as OperationCostEstimationItem;
			
			return entity;
		}

		public OperationCostEstimationItem FindByPrimaryKey(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo, System.String itemGroupID, System.String sRBillingGroup)
		{
			return base.FindByPrimaryKey(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo, itemGroupID, sRBillingGroup) as OperationCostEstimationItem;
		}


		#region IEnumerable<OperationCostEstimationItem> Members

		IEnumerator<OperationCostEstimationItem> IEnumerable<OperationCostEstimationItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OperationCostEstimationItem;
			}
		}

		#endregion
		
		private OperationCostEstimationItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OperationCostEstimationItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OperationCostEstimationItem ({DiagnoseID},{ProcedureID},{SRProcedureCategory},{ClassID},{RegistrationNo},{ItemGroupID},{SRBillingGroup})")]
	[Serializable]
	public partial class OperationCostEstimationItem : esOperationCostEstimationItem
	{
		public OperationCostEstimationItem()
		{

		}
	
		public OperationCostEstimationItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OperationCostEstimationItemMetadata.Meta();
			}
		}
		
		
		
		override protected esOperationCostEstimationItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationCostEstimationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OperationCostEstimationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationCostEstimationItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OperationCostEstimationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OperationCostEstimationItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OperationCostEstimationItemQuery : esOperationCostEstimationItemQuery
	{
		public OperationCostEstimationItemQuery()
		{

		}		
		
		public OperationCostEstimationItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OperationCostEstimationItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class OperationCostEstimationItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OperationCostEstimationItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.DiagnoseID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.DiagnoseID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.ProcedureID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.ProcedureID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.SRProcedureCategory, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.SRProcedureCategory;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.ItemGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.ItemGroupName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.ItemGroupName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.SRBillingGroup, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.SRBillingGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.CostAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.CostAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OperationCostEstimationItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationCostEstimationItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OperationCostEstimationItemMetadata Meta()
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
			 public const string DiagnoseID = "DiagnoseID";
			 public const string ProcedureID = "ProcedureID";
			 public const string SRProcedureCategory = "SRProcedureCategory";
			 public const string ClassID = "ClassID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ItemGroupID = "ItemGroupID";
			 public const string ItemGroupName = "ItemGroupName";
			 public const string SRBillingGroup = "SRBillingGroup";
			 public const string CostAmount = "CostAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DiagnoseID = "DiagnoseID";
			 public const string ProcedureID = "ProcedureID";
			 public const string SRProcedureCategory = "SRProcedureCategory";
			 public const string ClassID = "ClassID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ItemGroupID = "ItemGroupID";
			 public const string ItemGroupName = "ItemGroupName";
			 public const string SRBillingGroup = "SRBillingGroup";
			 public const string CostAmount = "CostAmount";
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
			lock (typeof(OperationCostEstimationItemMetadata))
			{
				if(OperationCostEstimationItemMetadata.mapDelegates == null)
				{
					OperationCostEstimationItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OperationCostEstimationItemMetadata.meta == null)
				{
					OperationCostEstimationItemMetadata.meta = new OperationCostEstimationItemMetadata();
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
				

				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedureCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBillingGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "OperationCostEstimationItem";
				meta.Destination = "OperationCostEstimationItem";
				
				meta.spInsert = "proc_OperationCostEstimationItemInsert";				
				meta.spUpdate = "proc_OperationCostEstimationItemUpdate";		
				meta.spDelete = "proc_OperationCostEstimationItemDelete";
				meta.spLoadAll = "proc_OperationCostEstimationItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_OperationCostEstimationItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OperationCostEstimationItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
