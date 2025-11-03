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
	abstract public class esDocumentSignatureItemCollection : esEntityCollectionWAuditLog
	{
		public esDocumentSignatureItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DocumentSignatureItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esDocumentSignatureItemQuery query)
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
			this.InitQuery(query as esDocumentSignatureItemQuery);
		}
		#endregion
		
		virtual public DocumentSignatureItem DetachEntity(DocumentSignatureItem entity)
		{
			return base.DetachEntity(entity) as DocumentSignatureItem;
		}
		
		virtual public DocumentSignatureItem AttachEntity(DocumentSignatureItem entity)
		{
			return base.AttachEntity(entity) as DocumentSignatureItem;
		}
		
		virtual public void Combine(DocumentSignatureItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DocumentSignatureItem this[int index]
		{
			get
			{
				return base[index] as DocumentSignatureItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DocumentSignatureItem);
		}
	}



	[Serializable]
	abstract public class esDocumentSignatureItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDocumentSignatureItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esDocumentSignatureItem()
		{

		}

		public esDocumentSignatureItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.Decimal minAmount)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, minAmount);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, minAmount);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.Decimal minAmount)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, minAmount);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, minAmount);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.Decimal minAmount)
		{
			esDocumentSignatureItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.MinAmount == minAmount);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.Decimal minAmount)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("MinAmount",minAmount);
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
						case "MinAmount": this.str.MinAmount = (string)value; break;							
						case "MaxAmount": this.str.MaxAmount = (string)value; break;							
						case "PositionAs1": this.str.PositionAs1 = (string)value; break;							
						case "PositionAs2": this.str.PositionAs2 = (string)value; break;							
						case "PositionAs3": this.str.PositionAs3 = (string)value; break;							
						case "PositionAs4": this.str.PositionAs4 = (string)value; break;							
						case "SignerBy1": this.str.SignerBy1 = (string)value; break;							
						case "SignerBy2": this.str.SignerBy2 = (string)value; break;							
						case "SignerBy3": this.str.SignerBy3 = (string)value; break;							
						case "SignerBy4": this.str.SignerBy4 = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MinAmount":
						
							if (value == null || value is System.Decimal)
								this.MinAmount = (System.Decimal?)value;
							break;
						
						case "MaxAmount":
						
							if (value == null || value is System.Decimal)
								this.MaxAmount = (System.Decimal?)value;
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
		/// Maps to DocumentSignatureItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.MinAmount
		/// </summary>
		virtual public System.Decimal? MinAmount
		{
			get
			{
				return base.GetSystemDecimal(DocumentSignatureItemMetadata.ColumnNames.MinAmount);
			}
			
			set
			{
				base.SetSystemDecimal(DocumentSignatureItemMetadata.ColumnNames.MinAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.MaxAmount
		/// </summary>
		virtual public System.Decimal? MaxAmount
		{
			get
			{
				return base.GetSystemDecimal(DocumentSignatureItemMetadata.ColumnNames.MaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(DocumentSignatureItemMetadata.ColumnNames.MaxAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.PositionAs1
		/// </summary>
		virtual public System.String PositionAs1
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs1);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs1, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.PositionAs2
		/// </summary>
		virtual public System.String PositionAs2
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs2);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs2, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.PositionAs3
		/// </summary>
		virtual public System.String PositionAs3
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs3);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs3, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.PositionAs4
		/// </summary>
		virtual public System.String PositionAs4
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs4);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.PositionAs4, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.SignerBy1
		/// </summary>
		virtual public System.String SignerBy1
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy1);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy1, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.SignerBy2
		/// </summary>
		virtual public System.String SignerBy2
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy2);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy2, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.SignerBy3
		/// </summary>
		virtual public System.String SignerBy3
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy3);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy3, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.SignerBy4
		/// </summary>
		virtual public System.String SignerBy4
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy4);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.SignerBy4, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DocumentSignatureItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DocumentSignatureItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to DocumentSignatureItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DocumentSignatureItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DocumentSignatureItemMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esDocumentSignatureItem entity)
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
				
			public System.String MinAmount
			{
				get
				{
					System.Decimal? data = entity.MinAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinAmount = null;
					else entity.MinAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxAmount
			{
				get
				{
					System.Decimal? data = entity.MaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxAmount = null;
					else entity.MaxAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String PositionAs1
			{
				get
				{
					System.String data = entity.PositionAs1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionAs1 = null;
					else entity.PositionAs1 = Convert.ToString(value);
				}
			}
				
			public System.String PositionAs2
			{
				get
				{
					System.String data = entity.PositionAs2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionAs2 = null;
					else entity.PositionAs2 = Convert.ToString(value);
				}
			}
				
			public System.String PositionAs3
			{
				get
				{
					System.String data = entity.PositionAs3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionAs3 = null;
					else entity.PositionAs3 = Convert.ToString(value);
				}
			}
				
			public System.String PositionAs4
			{
				get
				{
					System.String data = entity.PositionAs4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionAs4 = null;
					else entity.PositionAs4 = Convert.ToString(value);
				}
			}
				
			public System.String SignerBy1
			{
				get
				{
					System.String data = entity.SignerBy1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignerBy1 = null;
					else entity.SignerBy1 = Convert.ToString(value);
				}
			}
				
			public System.String SignerBy2
			{
				get
				{
					System.String data = entity.SignerBy2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignerBy2 = null;
					else entity.SignerBy2 = Convert.ToString(value);
				}
			}
				
			public System.String SignerBy3
			{
				get
				{
					System.String data = entity.SignerBy3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignerBy3 = null;
					else entity.SignerBy3 = Convert.ToString(value);
				}
			}
				
			public System.String SignerBy4
			{
				get
				{
					System.String data = entity.SignerBy4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignerBy4 = null;
					else entity.SignerBy4 = Convert.ToString(value);
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
			

			private esDocumentSignatureItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDocumentSignatureItemQuery query)
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
				throw new Exception("esDocumentSignatureItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DocumentSignatureItem : esDocumentSignatureItem
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
	abstract public class esDocumentSignatureItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DocumentSignatureItemMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MinAmount
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.MinAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MaxAmount
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.MaxAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PositionAs1
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.PositionAs1, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionAs2
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.PositionAs2, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionAs3
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.PositionAs3, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionAs4
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.PositionAs4, esSystemType.String);
			}
		} 
		
		public esQueryItem SignerBy1
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.SignerBy1, esSystemType.String);
			}
		} 
		
		public esQueryItem SignerBy2
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.SignerBy2, esSystemType.String);
			}
		} 
		
		public esQueryItem SignerBy3
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.SignerBy3, esSystemType.String);
			}
		} 
		
		public esQueryItem SignerBy4
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.SignerBy4, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DocumentSignatureItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DocumentSignatureItemCollection")]
	public partial class DocumentSignatureItemCollection : esDocumentSignatureItemCollection, IEnumerable<DocumentSignatureItem>
	{
		public DocumentSignatureItemCollection()
		{

		}
		
		public static implicit operator List<DocumentSignatureItem>(DocumentSignatureItemCollection coll)
		{
			List<DocumentSignatureItem> list = new List<DocumentSignatureItem>();
			
			foreach (DocumentSignatureItem emp in coll)
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
				return  DocumentSignatureItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentSignatureItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DocumentSignatureItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DocumentSignatureItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DocumentSignatureItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentSignatureItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DocumentSignatureItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DocumentSignatureItem AddNew()
		{
			DocumentSignatureItem entity = base.AddNewEntity() as DocumentSignatureItem;
			
			return entity;
		}

		public DocumentSignatureItem FindByPrimaryKey(System.String transactionNo, System.Decimal minAmount)
		{
			return base.FindByPrimaryKey(transactionNo, minAmount) as DocumentSignatureItem;
		}


		#region IEnumerable<DocumentSignatureItem> Members

		IEnumerator<DocumentSignatureItem> IEnumerable<DocumentSignatureItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DocumentSignatureItem;
			}
		}

		#endregion
		
		private DocumentSignatureItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DocumentSignatureItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DocumentSignatureItem ({TransactionNo},{MinAmount})")]
	[Serializable]
	public partial class DocumentSignatureItem : esDocumentSignatureItem
	{
		public DocumentSignatureItem()
		{

		}
	
		public DocumentSignatureItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DocumentSignatureItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDocumentSignatureItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DocumentSignatureItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DocumentSignatureItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DocumentSignatureItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DocumentSignatureItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DocumentSignatureItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DocumentSignatureItemQuery : esDocumentSignatureItemQuery
	{
		public DocumentSignatureItemQuery()
		{

		}		
		
		public DocumentSignatureItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DocumentSignatureItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class DocumentSignatureItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DocumentSignatureItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.MinAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.MinAmount;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.MaxAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.MaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.PositionAs1, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.PositionAs1;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.PositionAs2, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.PositionAs2;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.PositionAs3, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.PositionAs3;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.PositionAs4, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.PositionAs4;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.SignerBy1, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.SignerBy1;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.SignerBy2, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.SignerBy2;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.SignerBy3, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.SignerBy3;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.SignerBy4, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.SignerBy4;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DocumentSignatureItemMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DocumentSignatureItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DocumentSignatureItemMetadata Meta()
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
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string PositionAs1 = "PositionAs1";
			 public const string PositionAs2 = "PositionAs2";
			 public const string PositionAs3 = "PositionAs3";
			 public const string PositionAs4 = "PositionAs4";
			 public const string SignerBy1 = "SignerBy1";
			 public const string SignerBy2 = "SignerBy2";
			 public const string SignerBy3 = "SignerBy3";
			 public const string SignerBy4 = "SignerBy4";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string PositionAs1 = "PositionAs1";
			 public const string PositionAs2 = "PositionAs2";
			 public const string PositionAs3 = "PositionAs3";
			 public const string PositionAs4 = "PositionAs4";
			 public const string SignerBy1 = "SignerBy1";
			 public const string SignerBy2 = "SignerBy2";
			 public const string SignerBy3 = "SignerBy3";
			 public const string SignerBy4 = "SignerBy4";
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
			lock (typeof(DocumentSignatureItemMetadata))
			{
				if(DocumentSignatureItemMetadata.mapDelegates == null)
				{
					DocumentSignatureItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DocumentSignatureItemMetadata.meta == null)
				{
					DocumentSignatureItemMetadata.meta = new DocumentSignatureItemMetadata();
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
				meta.AddTypeMap("MinAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PositionAs1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionAs2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionAs3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionAs4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignerBy1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignerBy2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignerBy3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignerBy4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "DocumentSignatureItem";
				meta.Destination = "DocumentSignatureItem";
				
				meta.spInsert = "proc_DocumentSignatureItemInsert";				
				meta.spUpdate = "proc_DocumentSignatureItemUpdate";		
				meta.spDelete = "proc_DocumentSignatureItemDelete";
				meta.spLoadAll = "proc_DocumentSignatureItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_DocumentSignatureItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DocumentSignatureItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
