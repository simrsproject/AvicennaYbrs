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
	abstract public class esParamedicFeeVerificationRentalRoomsCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeVerificationRentalRoomsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeVerificationRentalRoomsCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeVerificationRentalRoomsQuery query)
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
			this.InitQuery(query as esParamedicFeeVerificationRentalRoomsQuery);
		}
		#endregion
		
		virtual public ParamedicFeeVerificationRentalRooms DetachEntity(ParamedicFeeVerificationRentalRooms entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeVerificationRentalRooms;
		}
		
		virtual public ParamedicFeeVerificationRentalRooms AttachEntity(ParamedicFeeVerificationRentalRooms entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeVerificationRentalRooms;
		}
		
		virtual public void Combine(ParamedicFeeVerificationRentalRoomsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeVerificationRentalRooms this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeVerificationRentalRooms;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeVerificationRentalRooms);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeVerificationRentalRooms : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeVerificationRentalRoomsQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeVerificationRentalRooms()
		{

		}

		public esParamedicFeeVerificationRentalRooms(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String verificationNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo, transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo, transactionNo, sequenceNo, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String verificationNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo, transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo, transactionNo, sequenceNo, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String verificationNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esParamedicFeeVerificationRentalRoomsQuery query = this.GetDynamicQuery();
			query.Where(query.VerificationNo == verificationNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String verificationNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("VerificationNo",verificationNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "VerificationNo": this.str.VerificationNo = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "RentalRoomsAmount": this.str.RentalRoomsAmount = (string)value; break;							
						case "TogethernessAmount": this.str.TogethernessAmount = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RentalRoomsAmount":
						
							if (value == null || value is System.Decimal)
								this.RentalRoomsAmount = (System.Decimal?)value;
							break;
						
						case "TogethernessAmount":
						
							if (value == null || value is System.Decimal)
								this.TogethernessAmount = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeVerificationRentalRooms.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.VerificationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.RentalRoomsAmount
		/// </summary>
		virtual public System.Decimal? RentalRoomsAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.RentalRoomsAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.RentalRoomsAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.TogethernessAmount
		/// </summary>
		virtual public System.Decimal? TogethernessAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TogethernessAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TogethernessAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeVerificationRentalRooms.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esParamedicFeeVerificationRentalRooms entity)
			{
				this.entity = entity;
			}
			
	
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
				}
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
				
			public System.String RentalRoomsAmount
			{
				get
				{
					System.Decimal? data = entity.RentalRoomsAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RentalRoomsAmount = null;
					else entity.RentalRoomsAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TogethernessAmount
			{
				get
				{
					System.Decimal? data = entity.TogethernessAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TogethernessAmount = null;
					else entity.TogethernessAmount = Convert.ToDecimal(value);
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
			

			private esParamedicFeeVerificationRentalRooms entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeVerificationRentalRoomsQuery query)
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
				throw new Exception("esParamedicFeeVerificationRentalRooms can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeVerificationRentalRooms : esParamedicFeeVerificationRentalRooms
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
	abstract public class esParamedicFeeVerificationRentalRoomsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeVerificationRentalRoomsMetadata.Meta();
			}
		}	
		

		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem RentalRoomsAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.RentalRoomsAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TogethernessAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TogethernessAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeVerificationRentalRoomsCollection")]
	public partial class ParamedicFeeVerificationRentalRoomsCollection : esParamedicFeeVerificationRentalRoomsCollection, IEnumerable<ParamedicFeeVerificationRentalRooms>
	{
		public ParamedicFeeVerificationRentalRoomsCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeVerificationRentalRooms>(ParamedicFeeVerificationRentalRoomsCollection coll)
		{
			List<ParamedicFeeVerificationRentalRooms> list = new List<ParamedicFeeVerificationRentalRooms>();
			
			foreach (ParamedicFeeVerificationRentalRooms emp in coll)
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
				return  ParamedicFeeVerificationRentalRoomsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeVerificationRentalRoomsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeVerificationRentalRooms(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeVerificationRentalRooms();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeVerificationRentalRoomsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeVerificationRentalRoomsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeVerificationRentalRoomsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeVerificationRentalRooms AddNew()
		{
			ParamedicFeeVerificationRentalRooms entity = base.AddNewEntity() as ParamedicFeeVerificationRentalRooms;
			
			return entity;
		}

		public ParamedicFeeVerificationRentalRooms FindByPrimaryKey(System.String verificationNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(verificationNo, transactionNo, sequenceNo, tariffComponentID) as ParamedicFeeVerificationRentalRooms;
		}


		#region IEnumerable<ParamedicFeeVerificationRentalRooms> Members

		IEnumerator<ParamedicFeeVerificationRentalRooms> IEnumerable<ParamedicFeeVerificationRentalRooms>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeVerificationRentalRooms;
			}
		}

		#endregion
		
		private ParamedicFeeVerificationRentalRoomsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeVerificationRentalRooms' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeVerificationRentalRooms ({VerificationNo},{TransactionNo},{SequenceNo},{TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeVerificationRentalRooms : esParamedicFeeVerificationRentalRooms
	{
		public ParamedicFeeVerificationRentalRooms()
		{

		}
	
		public ParamedicFeeVerificationRentalRooms(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeVerificationRentalRoomsMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeVerificationRentalRoomsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeVerificationRentalRoomsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeVerificationRentalRoomsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeVerificationRentalRoomsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeVerificationRentalRoomsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeVerificationRentalRoomsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeVerificationRentalRoomsQuery : esParamedicFeeVerificationRentalRoomsQuery
	{
		public ParamedicFeeVerificationRentalRoomsQuery()
		{

		}		
		
		public ParamedicFeeVerificationRentalRoomsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeVerificationRentalRoomsQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeVerificationRentalRoomsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeVerificationRentalRoomsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.VerificationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.VerificationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.RentalRoomsAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.RentalRoomsAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.TogethernessAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.TogethernessAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeVerificationRentalRoomsMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationRentalRoomsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeVerificationRentalRoomsMetadata Meta()
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
			 public const string VerificationNo = "VerificationNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string RentalRoomsAmount = "RentalRoomsAmount";
			 public const string TogethernessAmount = "TogethernessAmount";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string VerificationNo = "VerificationNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string RentalRoomsAmount = "RentalRoomsAmount";
			 public const string TogethernessAmount = "TogethernessAmount";
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
			lock (typeof(ParamedicFeeVerificationRentalRoomsMetadata))
			{
				if(ParamedicFeeVerificationRentalRoomsMetadata.mapDelegates == null)
				{
					ParamedicFeeVerificationRentalRoomsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeVerificationRentalRoomsMetadata.meta == null)
				{
					ParamedicFeeVerificationRentalRoomsMetadata.meta = new ParamedicFeeVerificationRentalRoomsMetadata();
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
				

				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RentalRoomsAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TogethernessAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "ParamedicFeeVerificationRentalRooms";
				meta.Destination = "ParamedicFeeVerificationRentalRooms";
				
				meta.spInsert = "proc_ParamedicFeeVerificationRentalRoomsInsert";				
				meta.spUpdate = "proc_ParamedicFeeVerificationRentalRoomsUpdate";		
				meta.spDelete = "proc_ParamedicFeeVerificationRentalRoomsDelete";
				meta.spLoadAll = "proc_ParamedicFeeVerificationRentalRoomsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeVerificationRentalRoomsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeVerificationRentalRoomsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
