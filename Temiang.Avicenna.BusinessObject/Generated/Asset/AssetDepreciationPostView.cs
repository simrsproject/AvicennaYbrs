/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2023 1:49:28 PM
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
	abstract public class esAssetDepreciationPostViewCollection : esEntityCollection
	{
		public esAssetDepreciationPostViewCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetDepreciationPostViewCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetDepreciationPostViewQuery query)
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
			this.InitQuery(query as esAssetDepreciationPostViewQuery);
		}
		#endregion

		virtual public AssetDepreciationPostView DetachEntity(AssetDepreciationPostView entity)
		{
			return base.DetachEntity(entity) as AssetDepreciationPostView;
		}

		virtual public AssetDepreciationPostView AttachEntity(AssetDepreciationPostView entity)
		{
			return base.AttachEntity(entity) as AssetDepreciationPostView;
		}

		virtual public void Combine(AssetDepreciationPostViewCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetDepreciationPostView this[int index]
		{
			get
			{
				return base[index] as AssetDepreciationPostView;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetDepreciationPostView);
		}
	}

	[Serializable]
	abstract public class esAssetDepreciationPostView : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetDepreciationPostViewQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetDepreciationPostView()
		{
		}

		public esAssetDepreciationPostView(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey

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
						case "AssetDepreciationPostId": this.str.AssetDepreciationPostId = (string)value; break;
						case "AssetGroupId": this.str.AssetGroupId = (string)value; break;
						case "GroupName": this.str.GroupName = (string)value; break;
						case "AssetID": this.str.AssetID = (string)value; break;
						case "AssetName": this.str.AssetName = (string)value; break;
						case "SerialNumber": this.str.SerialNumber = (string)value; break;
						case "PeriodNo": this.str.PeriodNo = (string)value; break;
						case "Year": this.str.Year = (string)value; break;
						case "Month": this.str.Month = (string)value; break;
						case "DepreciationAmount": this.str.DepreciationAmount = (string)value; break;
						case "IsPosted": this.str.IsPosted = (string)value; break;
						case "JournalId": this.str.JournalId = (string)value; break;
						case "AssetAccumulationAccountId": this.str.AssetAccumulationAccountId = (string)value; break;
						case "AssetAccumulationSubLedgerId": this.str.AssetAccumulationSubLedgerId = (string)value; break;
						case "AssetCostAccountId": this.str.AssetCostAccountId = (string)value; break;
						case "AssetCostSubLedgerId": this.str.AssetCostSubLedgerId = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "AssetDepreciationPostId":

							if (value == null || value is System.Int32)
								this.AssetDepreciationPostId = (System.Int32?)value;
							break;
						case "PeriodNo":

							if (value == null || value is System.Int32)
								this.PeriodNo = (System.Int32?)value;
							break;
						case "DepreciationAmount":

							if (value == null || value is System.Decimal)
								this.DepreciationAmount = (System.Decimal?)value;
							break;
						case "IsPosted":

							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						case "JournalId":

							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						case "AssetAccumulationAccountId":

							if (value == null || value is System.Int32)
								this.AssetAccumulationAccountId = (System.Int32?)value;
							break;
						case "AssetAccumulationSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetAccumulationSubLedgerId = (System.Int32?)value;
							break;
						case "AssetCostAccountId":

							if (value == null || value is System.Int32)
								this.AssetCostAccountId = (System.Int32?)value;
							break;
						case "AssetCostSubLedgerId":

							if (value == null || value is System.Int32)
								this.AssetCostSubLedgerId = (System.Int32?)value;
							break;
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
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
		/// Maps to AssetDepreciationPostView.AssetDepreciationPostId
		/// </summary>
		virtual public System.Int32? AssetDepreciationPostId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetDepreciationPostId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetDepreciationPostId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetGroupId
		/// </summary>
		virtual public System.String AssetGroupId
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetGroupId);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetGroupId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.GroupName
		/// </summary>
		virtual public System.String GroupName
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.GroupName);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.GroupName, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetID);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetName
		/// </summary>
		virtual public System.String AssetName
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetName);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.AssetName, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.SerialNumber
		/// </summary>
		virtual public System.String SerialNumber
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.SerialNumber);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.SerialNumber, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.PeriodNo
		/// </summary>
		virtual public System.Int32? PeriodNo
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.PeriodNo);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.PeriodNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.Year);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.Year, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.Month);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.Month, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.DepreciationAmount
		/// </summary>
		virtual public System.Decimal? DepreciationAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetDepreciationPostViewMetadata.ColumnNames.DepreciationAmount);
			}

			set
			{
				base.SetSystemDecimal(AssetDepreciationPostViewMetadata.ColumnNames.DepreciationAmount, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(AssetDepreciationPostViewMetadata.ColumnNames.IsPosted);
			}

			set
			{
				base.SetSystemBoolean(AssetDepreciationPostViewMetadata.ColumnNames.IsPosted, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.JournalId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetAccumulationAccountId
		/// </summary>
		virtual public System.Int32? AssetAccumulationAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetAccumulationSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetAccumulationSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetCostAccountId
		/// </summary>
		virtual public System.Int32? AssetCostAccountId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostAccountId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostAccountId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.AssetCostSubLedgerId
		/// </summary>
		virtual public System.Int32? AssetCostSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetDepreciationPostViewMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AssetDepreciationPostView.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(AssetDepreciationPostViewMetadata.ColumnNames.SubLedgerId, value);
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
			public esStrings(esAssetDepreciationPostView entity)
			{
				this.entity = entity;
			}
			public System.String AssetDepreciationPostId
			{
				get
				{
					System.Int32? data = entity.AssetDepreciationPostId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetDepreciationPostId = null;
					else entity.AssetDepreciationPostId = Convert.ToInt32(value);
				}
			}
			public System.String AssetGroupId
			{
				get
				{
					System.String data = entity.AssetGroupId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetGroupId = null;
					else entity.AssetGroupId = Convert.ToString(value);
				}
			}
			public System.String GroupName
			{
				get
				{
					System.String data = entity.GroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupName = null;
					else entity.GroupName = Convert.ToString(value);
				}
			}
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
			public System.String AssetName
			{
				get
				{
					System.String data = entity.AssetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetName = null;
					else entity.AssetName = Convert.ToString(value);
				}
			}
			public System.String SerialNumber
			{
				get
				{
					System.String data = entity.SerialNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SerialNumber = null;
					else entity.SerialNumber = Convert.ToString(value);
				}
			}
			public System.String PeriodNo
			{
				get
				{
					System.Int32? data = entity.PeriodNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodNo = null;
					else entity.PeriodNo = Convert.ToInt32(value);
				}
			}
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
			public System.String DepreciationAmount
			{
				get
				{
					System.Decimal? data = entity.DepreciationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationAmount = null;
					else entity.DepreciationAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsPosted
			{
				get
				{
					System.Boolean? data = entity.IsPosted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosted = null;
					else entity.IsPosted = Convert.ToBoolean(value);
				}
			}
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
			public System.String AssetAccumulationAccountId
			{
				get
				{
					System.Int32? data = entity.AssetAccumulationAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetAccumulationAccountId = null;
					else entity.AssetAccumulationAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetAccumulationSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetAccumulationSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetAccumulationSubLedgerId = null;
					else entity.AssetAccumulationSubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String AssetCostAccountId
			{
				get
				{
					System.Int32? data = entity.AssetCostAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostAccountId = null;
					else entity.AssetCostAccountId = Convert.ToInt32(value);
				}
			}
			public System.String AssetCostSubLedgerId
			{
				get
				{
					System.Int32? data = entity.AssetCostSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetCostSubLedgerId = null;
					else entity.AssetCostSubLedgerId = Convert.ToInt32(value);
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
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
				}
			}
			private esAssetDepreciationPostView entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetDepreciationPostViewQuery query)
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
				throw new Exception("esAssetDepreciationPostView can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetDepreciationPostView : esAssetDepreciationPostView
	{
	}

	[Serializable]
	abstract public class esAssetDepreciationPostViewQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationPostViewMetadata.Meta();
			}
		}

		public esQueryItem AssetDepreciationPostId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetDepreciationPostId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetGroupId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetGroupId, esSystemType.String);
			}
		}

		public esQueryItem GroupName
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.GroupName, esSystemType.String);
			}
		}

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		}

		public esQueryItem AssetName
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetName, esSystemType.String);
			}
		}

		public esQueryItem SerialNumber
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.SerialNumber, esSystemType.String);
			}
		}

		public esQueryItem PeriodNo
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.PeriodNo, esSystemType.Int32);
			}
		}

		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.Year, esSystemType.String);
			}
		}

		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.Month, esSystemType.String);
			}
		}

		public esQueryItem DepreciationAmount
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.DepreciationAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		}

		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetAccumulationAccountId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetAccumulationSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetCostAccountId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetCostAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem AssetCostSubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.AssetCostSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostViewMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetDepreciationPostViewCollection")]
	public partial class AssetDepreciationPostViewCollection : esAssetDepreciationPostViewCollection, IEnumerable<AssetDepreciationPostView>
	{
		public AssetDepreciationPostViewCollection()
		{

		}

		public static implicit operator List<AssetDepreciationPostView>(AssetDepreciationPostViewCollection coll)
		{
			List<AssetDepreciationPostView> list = new List<AssetDepreciationPostView>();

			foreach (AssetDepreciationPostView emp in coll)
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
				return AssetDepreciationPostViewMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationPostViewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetDepreciationPostView(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetDepreciationPostView();
		}

		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetDepreciationPostViewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationPostViewQuery();
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
		public bool Load(AssetDepreciationPostViewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetDepreciationPostView AddNew()
		{
			AssetDepreciationPostView entity = base.AddNewEntity() as AssetDepreciationPostView;

			return entity;
		}

		#region IEnumerable< AssetDepreciationPostView> Members

		IEnumerator<AssetDepreciationPostView> IEnumerable<AssetDepreciationPostView>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetDepreciationPostView;
			}
		}

		#endregion

		private AssetDepreciationPostViewQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetDepreciationPostView' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetDepreciationPostView ()")]
	[Serializable]
	public partial class AssetDepreciationPostView : esAssetDepreciationPostView
	{
		public AssetDepreciationPostView()
		{
		}

		public AssetDepreciationPostView(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationPostViewMetadata.Meta();
			}
		}

		override protected esAssetDepreciationPostViewQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationPostViewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetDepreciationPostViewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationPostViewQuery();
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
		public bool Load(AssetDepreciationPostViewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetDepreciationPostViewQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetDepreciationPostViewQuery : esAssetDepreciationPostViewQuery
	{
		public AssetDepreciationPostViewQuery()
		{

		}

		public AssetDepreciationPostViewQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetDepreciationPostViewQuery";
		}
	}

	[Serializable]
	public partial class AssetDepreciationPostViewMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetDepreciationPostViewMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetDepreciationPostId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetDepreciationPostId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetGroupId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetGroupId;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.GroupName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.GroupName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.SerialNumber, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.SerialNumber;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.PeriodNo, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.PeriodNo;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.Year, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.Month, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.DepreciationAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.DepreciationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.IsPosted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.IsPosted;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.JournalId, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationAccountId, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetAccumulationAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetAccumulationSubLedgerId, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetAccumulationSubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostAccountId, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetCostAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.AssetCostSubLedgerId, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.AssetCostSubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.ServiceUnitID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetDepreciationPostViewMetadata.ColumnNames.SubLedgerId, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostViewMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);


		}
		#endregion

		static public AssetDepreciationPostViewMetadata Meta()
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
			public const string AssetDepreciationPostId = "AssetDepreciationPostId";
			public const string AssetGroupId = "AssetGroupId";
			public const string GroupName = "GroupName";
			public const string AssetID = "AssetID";
			public const string AssetName = "AssetName";
			public const string SerialNumber = "SerialNumber";
			public const string PeriodNo = "PeriodNo";
			public const string Year = "Year";
			public const string Month = "Month";
			public const string DepreciationAmount = "DepreciationAmount";
			public const string IsPosted = "IsPosted";
			public const string JournalId = "JournalId";
			public const string AssetAccumulationAccountId = "AssetAccumulationAccountId";
			public const string AssetAccumulationSubLedgerId = "AssetAccumulationSubLedgerId";
			public const string AssetCostAccountId = "AssetCostAccountId";
			public const string AssetCostSubLedgerId = "AssetCostSubLedgerId";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SubLedgerId = "SubLedgerId";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssetDepreciationPostId = "AssetDepreciationPostId";
			public const string AssetGroupId = "AssetGroupId";
			public const string GroupName = "GroupName";
			public const string AssetID = "AssetID";
			public const string AssetName = "AssetName";
			public const string SerialNumber = "SerialNumber";
			public const string PeriodNo = "PeriodNo";
			public const string Year = "Year";
			public const string Month = "Month";
			public const string DepreciationAmount = "DepreciationAmount";
			public const string IsPosted = "IsPosted";
			public const string JournalId = "JournalId";
			public const string AssetAccumulationAccountId = "AssetAccumulationAccountId";
			public const string AssetAccumulationSubLedgerId = "AssetAccumulationSubLedgerId";
			public const string AssetCostAccountId = "AssetCostAccountId";
			public const string AssetCostSubLedgerId = "AssetCostSubLedgerId";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SubLedgerId = "SubLedgerId";
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
			lock (typeof(AssetDepreciationPostViewMetadata))
			{
				if (AssetDepreciationPostViewMetadata.mapDelegates == null)
				{
					AssetDepreciationPostViewMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetDepreciationPostViewMetadata.meta == null)
				{
					AssetDepreciationPostViewMetadata.meta = new AssetDepreciationPostViewMetadata();
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

				meta.AddTypeMap("AssetDepreciationPostId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetGroupId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GroupName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SerialNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Month", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetAccumulationAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetAccumulationSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetCostAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetCostSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));


				meta.Source = "AssetDepreciationPostView";
				meta.Destination = "AssetDepreciationPostView";
				meta.spInsert = "proc_AssetDepreciationPostViewInsert";
				meta.spUpdate = "proc_AssetDepreciationPostViewUpdate";
				meta.spDelete = "proc_AssetDepreciationPostViewDelete";
				meta.spLoadAll = "proc_AssetDepreciationPostViewLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetDepreciationPostViewLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetDepreciationPostViewMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
