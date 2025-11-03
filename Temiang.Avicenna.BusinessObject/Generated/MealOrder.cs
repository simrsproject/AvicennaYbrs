/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/20/2020 2:04:53 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esMealOrderCollection : esEntityCollectionWAuditLog
	{
		public esMealOrderCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MealOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealOrderQuery query)
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
			this.InitQuery(query as esMealOrderQuery);
		}
		#endregion

		virtual public MealOrder DetachEntity(MealOrder entity)
		{
			return base.DetachEntity(entity) as MealOrder;
		}

		virtual public MealOrder AttachEntity(MealOrder entity)
		{
			return base.AttachEntity(entity) as MealOrder;
		}

		virtual public void Combine(MealOrderCollection collection)
		{
			base.Combine(collection);
		}

		new public MealOrder this[int index]
		{
			get
			{
				return base[index] as MealOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealOrder);
		}
	}

	[Serializable]
	abstract public class esMealOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealOrder()
		{
		}

		public esMealOrder(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo)
		{
			esMealOrderQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "OrderDate": this.str.OrderDate = (string)value; break;
						case "EffectiveDate": this.str.EffectiveDate = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "DietPatientNo": this.str.DietPatientNo = (string)value; break;
						case "DietID": this.str.DietID = (string)value; break;
						case "MenuID": this.str.MenuID = (string)value; break;
						case "MenuItemID": this.str.MenuItemID = (string)value; break;
						case "FastingTime": this.str.FastingTime = (string)value; break;
						case "IsAdditional": this.str.IsAdditional = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VersionID": this.str.VersionID = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "IsOpr": this.str.IsOpr = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "OrderDate":

							if (value == null || value is System.DateTime)
								this.OrderDate = (System.DateTime?)value;
							break;
						case "EffectiveDate":

							if (value == null || value is System.DateTime)
								this.EffectiveDate = (System.DateTime?)value;
							break;
						case "IsAdditional":

							if (value == null || value is System.Boolean)
								this.IsAdditional = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "IsOpr":

							if (value == null || value is System.Boolean)
								this.IsOpr = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to MealOrder.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.OrderDate
		/// </summary>
		virtual public System.DateTime? OrderDate
		{
			get
			{
				return base.GetSystemDateTime(MealOrderMetadata.ColumnNames.OrderDate);
			}

			set
			{
				base.SetSystemDateTime(MealOrderMetadata.ColumnNames.OrderDate, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.EffectiveDate
		/// </summary>
		virtual public System.DateTime? EffectiveDate
		{
			get
			{
				return base.GetSystemDateTime(MealOrderMetadata.ColumnNames.EffectiveDate);
			}

			set
			{
				base.SetSystemDateTime(MealOrderMetadata.ColumnNames.EffectiveDate, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.DietPatientNo
		/// </summary>
		virtual public System.String DietPatientNo
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.DietPatientNo);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.DietPatientNo, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.DietID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.DietID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.MenuID
		/// </summary>
		virtual public System.String MenuID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.MenuID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.MenuID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.MenuItemID
		/// </summary>
		virtual public System.String MenuItemID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.MenuItemID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.MenuItemID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.FastingTime
		/// </summary>
		virtual public System.String FastingTime
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.FastingTime);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.FastingTime, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.IsAdditional
		/// </summary>
		virtual public System.Boolean? IsAdditional
		{
			get
			{
				return base.GetSystemBoolean(MealOrderMetadata.ColumnNames.IsAdditional);
			}

			set
			{
				base.SetSystemBoolean(MealOrderMetadata.ColumnNames.IsAdditional, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(MealOrderMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(MealOrderMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(MealOrderMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(MealOrderMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealOrderMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MealOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.VersionID
		/// </summary>
		virtual public System.String VersionID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.VersionID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.VersionID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(MealOrderMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(MealOrderMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealOrderMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(MealOrderMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(MealOrderMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(MealOrderMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MealOrder.IsOpr
		/// </summary>
		virtual public System.Boolean? IsOpr
		{
			get
			{
				return base.GetSystemBoolean(MealOrderMetadata.ColumnNames.IsOpr);
			}

			set
			{
				base.SetSystemBoolean(MealOrderMetadata.ColumnNames.IsOpr, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
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
			public esStrings(esMealOrder entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String OrderDate
			{
				get
				{
					System.DateTime? data = entity.OrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDate = null;
					else entity.OrderDate = Convert.ToDateTime(value);
				}
			}
			public System.String EffectiveDate
			{
				get
				{
					System.DateTime? data = entity.EffectiveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveDate = null;
					else entity.EffectiveDate = Convert.ToDateTime(value);
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
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
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
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String DietPatientNo
			{
				get
				{
					System.String data = entity.DietPatientNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietPatientNo = null;
					else entity.DietPatientNo = Convert.ToString(value);
				}
			}
			public System.String DietID
			{
				get
				{
					System.String data = entity.DietID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietID = null;
					else entity.DietID = Convert.ToString(value);
				}
			}
			public System.String MenuID
			{
				get
				{
					System.String data = entity.MenuID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MenuID = null;
					else entity.MenuID = Convert.ToString(value);
				}
			}
			public System.String MenuItemID
			{
				get
				{
					System.String data = entity.MenuItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MenuItemID = null;
					else entity.MenuItemID = Convert.ToString(value);
				}
			}
			public System.String FastingTime
			{
				get
				{
					System.String data = entity.FastingTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FastingTime = null;
					else entity.FastingTime = Convert.ToString(value);
				}
			}
			public System.String IsAdditional
			{
				get
				{
					System.Boolean? data = entity.IsAdditional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdditional = null;
					else entity.IsAdditional = Convert.ToBoolean(value);
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
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			public System.String VersionID
			{
				get
				{
					System.String data = entity.VersionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VersionID = null;
					else entity.VersionID = Convert.ToString(value);
				}
			}
			public System.String SeqNo
			{
				get
				{
					System.String data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToString(value);
				}
			}
			public System.String IsVerified
			{
				get
				{
					System.Boolean? data = entity.IsVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerified = null;
					else entity.IsVerified = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsOpr
			{
				get
				{
					System.Boolean? data = entity.IsOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpr = null;
					else entity.IsOpr = Convert.ToBoolean(value);
				}
			}
			private esMealOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealOrderQuery query)
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
				throw new Exception("esMealOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MealOrder : esMealOrder
	{
	}

	[Serializable]
	abstract public class esMealOrderQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MealOrderMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem OrderDate
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EffectiveDate
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.EffectiveDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem DietPatientNo
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.DietPatientNo, esSystemType.String);
			}
		}

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.DietID, esSystemType.String);
			}
		}

		public esQueryItem MenuID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.MenuID, esSystemType.String);
			}
		}

		public esQueryItem MenuItemID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.MenuItemID, esSystemType.String);
			}
		}

		public esQueryItem FastingTime
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.FastingTime, esSystemType.String);
			}
		}

		public esQueryItem IsAdditional
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.IsAdditional, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem VersionID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.VersionID, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsOpr
		{
			get
			{
				return new esQueryItem(this, MealOrderMetadata.ColumnNames.IsOpr, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealOrderCollection")]
	public partial class MealOrderCollection : esMealOrderCollection, IEnumerable<MealOrder>
	{
		public MealOrderCollection()
		{

		}

		public static implicit operator List<MealOrder>(MealOrderCollection coll)
		{
			List<MealOrder> list = new List<MealOrder>();

			foreach (MealOrder emp in coll)
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
				return MealOrderMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealOrder();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MealOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(MealOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MealOrder AddNew()
		{
			MealOrder entity = base.AddNewEntity() as MealOrder;

			return entity;
		}
		public MealOrder FindByPrimaryKey(String orderNo)
		{
			return base.FindByPrimaryKey(orderNo) as MealOrder;
		}

		#region IEnumerable< MealOrder> Members

		IEnumerator<MealOrder> IEnumerable<MealOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MealOrder;
			}
		}

		#endregion

		private MealOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealOrder' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MealOrder ({OrderNo})")]
	[Serializable]
	public partial class MealOrder : esMealOrder
	{
		public MealOrder()
		{
		}

		public MealOrder(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderMetadata.Meta();
			}
		}

		override protected esMealOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MealOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(MealOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MealOrderQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MealOrderQuery : esMealOrderQuery
	{
		public MealOrderQuery()
		{

		}

		public MealOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MealOrderQuery";
		}
	}

	[Serializable]
	public partial class MealOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.OrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderMetadata.PropertyNames.OrderDate;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.EffectiveDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderMetadata.PropertyNames.EffectiveDate;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.ClassID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.BedID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.DietPatientNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.DietPatientNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.DietID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.DietID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.MenuID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.MenuID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.MenuItemID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.MenuItemID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.FastingTime, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.FastingTime;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.IsAdditional, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderMetadata.PropertyNames.IsAdditional;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.IsApproved, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.VersionID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.VersionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.SeqNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.SeqNo;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.IsVerified, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.VerifiedDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.VerifiedByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MealOrderMetadata.ColumnNames.IsOpr, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderMetadata.PropertyNames.IsOpr;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MealOrderMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string OrderNo = "OrderNo";
			public const string OrderDate = "OrderDate";
			public const string EffectiveDate = "EffectiveDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ClassID = "ClassID";
			public const string BedID = "BedID";
			public const string DietPatientNo = "DietPatientNo";
			public const string DietID = "DietID";
			public const string MenuID = "MenuID";
			public const string MenuItemID = "MenuItemID";
			public const string FastingTime = "FastingTime";
			public const string IsAdditional = "IsAdditional";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VersionID = "VersionID";
			public const string SeqNo = "SeqNo";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string IsOpr = "IsOpr";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string OrderDate = "OrderDate";
			public const string EffectiveDate = "EffectiveDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ClassID = "ClassID";
			public const string BedID = "BedID";
			public const string DietPatientNo = "DietPatientNo";
			public const string DietID = "DietID";
			public const string MenuID = "MenuID";
			public const string MenuItemID = "MenuItemID";
			public const string FastingTime = "FastingTime";
			public const string IsAdditional = "IsAdditional";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VersionID = "VersionID";
			public const string SeqNo = "SeqNo";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string IsOpr = "IsOpr";
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
			lock (typeof(MealOrderMetadata))
			{
				if (MealOrderMetadata.mapDelegates == null)
				{
					MealOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MealOrderMetadata.meta == null)
				{
					MealOrderMetadata.meta = new MealOrderMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EffectiveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietPatientNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MenuItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FastingTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAdditional", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VersionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOpr", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "MealOrder";
				meta.Destination = "MealOrder";
				meta.spInsert = "proc_MealOrderInsert";
				meta.spUpdate = "proc_MealOrderUpdate";
				meta.spDelete = "proc_MealOrderDelete";
				meta.spLoadAll = "proc_MealOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealOrderLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
