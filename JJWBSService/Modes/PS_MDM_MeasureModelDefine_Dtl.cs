﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace JJWBSService.Modes
{
	/// <summary>
	/// 实体类PS_MDM_MeasureModelDefine_Dtl。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("PS_MDM_MeasureModelDefine_Dtl")]
	[Serializable]
	public partial class PS_MDM_MeasureModelDefine_Dtl : Entity
	{
		#region Model
		private Guid _Id;
		private Guid? _MasterId;
		private int? _Sequ;
		private string _Step;
		private float? _Weight;
		private float? _TotalWeight;
		private float? _Quantities;
		private bool? _isMain;
		private DateTime? _UpdDate;
		private string _Memo;
		private Guid? _rsrc_guid;
		private string _rsrc_code;
		private string _rsrc_name;
		private string _KeyWord;
		private string _FormId;

		/// <summary>
		/// 
		/// </summary>
		[Field("Id")]
		public Guid Id
		{
			get { return _Id; }
			set
			{
				this.OnPropertyValueChange("Id");
				this._Id = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("MasterId")]
		public Guid? MasterId
		{
			get { return _MasterId; }
			set
			{
				this.OnPropertyValueChange("MasterId");
				this._MasterId = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Sequ")]
		public int? Sequ
		{
			get { return _Sequ; }
			set
			{
				this.OnPropertyValueChange("Sequ");
				this._Sequ = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Step")]
		public string Step
		{
			get { return _Step; }
			set
			{
				this.OnPropertyValueChange("Step");
				this._Step = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Weight")]
		public float? Weight
		{
			get { return _Weight; }
			set
			{
				this.OnPropertyValueChange("Weight");
				this._Weight = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("TotalWeight")]
		public float? TotalWeight
		{
			get { return _TotalWeight; }
			set
			{
				this.OnPropertyValueChange("TotalWeight");
				this._TotalWeight = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Quantities")]
		public float? Quantities
		{
			get { return _Quantities; }
			set
			{
				this.OnPropertyValueChange("Quantities");
				this._Quantities = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("isMain")]
		public bool? isMain
		{
			get { return _isMain; }
			set
			{
				this.OnPropertyValueChange("isMain");
				this._isMain = value;
			}
		}
		/// <summary>
		/// 最后更新时间
		/// </summary>
		[Field("UpdDate")]
		public DateTime? UpdDate
		{
			get { return _UpdDate; }
			set
			{
				this.OnPropertyValueChange("UpdDate");
				this._UpdDate = value;
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		[Field("Memo")]
		public string Memo
		{
			get { return _Memo; }
			set
			{
				this.OnPropertyValueChange("Memo");
				this._Memo = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("rsrc_guid")]
		public Guid? rsrc_guid
		{
			get { return _rsrc_guid; }
			set
			{
				this.OnPropertyValueChange("rsrc_guid");
				this._rsrc_guid = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("rsrc_code")]
		public string rsrc_code
		{
			get { return _rsrc_code; }
			set
			{
				this.OnPropertyValueChange("rsrc_code");
				this._rsrc_code = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("rsrc_name")]
		public string rsrc_name
		{
			get { return _rsrc_name; }
			set
			{
				this.OnPropertyValueChange("rsrc_name");
				this._rsrc_name = value;
			}
		}
		/// <summary>
		/// 对应业务对象
		/// </summary>
		[Field("KeyWord")]
		public string KeyWord
		{
			get { return _KeyWord; }
			set
			{
				this.OnPropertyValueChange("KeyWord");
				this._KeyWord = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("FormId")]
		public string FormId
		{
			get { return _FormId; }
			set
			{
				this.OnPropertyValueChange("FormId");
				this._FormId = value;
			}
		}
		#endregion

		#region Method
		/// <summary>
		/// 获取实体中的主键列
		/// </summary>
		public override Field[] GetPrimaryKeyFields()
		{
			return new Field[] {
				_.Id,
			};
		}
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.Id,
				_.MasterId,
				_.Sequ,
				_.Step,
				_.Weight,
				_.TotalWeight,
				_.Quantities,
				_.isMain,
				_.UpdDate,
				_.Memo,
				_.rsrc_guid,
				_.rsrc_code,
				_.rsrc_name,
				_.KeyWord,
				_.FormId,
			};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._Id,
				this._MasterId,
				this._Sequ,
				this._Step,
				this._Weight,
				this._TotalWeight,
				this._Quantities,
				this._isMain,
				this._UpdDate,
				this._Memo,
				this._rsrc_guid,
				this._rsrc_code,
				this._rsrc_name,
				this._KeyWord,
				this._FormId,
			};
		}
		/// <summary>
		/// 是否是v1.10.5.6及以上版本实体。
		/// </summary>
		/// <returns></returns>
		public override bool V1_10_5_6_Plus()
		{
			return true;
		}
		#endregion

		#region _Field
		/// <summary>
		/// 字段信息
		/// </summary>
		public class _
		{
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*", "PS_MDM_MeasureModelDefine_Dtl");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MasterId = new Field("MasterId", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Sequ = new Field("Sequ", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Step = new Field("Step", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Weight = new Field("Weight", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field TotalWeight = new Field("TotalWeight", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Quantities = new Field("Quantities", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field isMain = new Field("isMain", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 最后更新时间
			/// </summary>
			public readonly static Field UpdDate = new Field("UpdDate", "PS_MDM_MeasureModelDefine_Dtl", "最后更新时间");
			/// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Memo = new Field("Memo", "PS_MDM_MeasureModelDefine_Dtl", "备注");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field rsrc_guid = new Field("rsrc_guid", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field rsrc_code = new Field("rsrc_code", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field rsrc_name = new Field("rsrc_name", "PS_MDM_MeasureModelDefine_Dtl", "");
			/// <summary>
			/// 对应业务对象
			/// </summary>
			public readonly static Field KeyWord = new Field("KeyWord", "PS_MDM_MeasureModelDefine_Dtl", "对应业务对象");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field FormId = new Field("FormId", "PS_MDM_MeasureModelDefine_Dtl", "");
		}
		#endregion
	}
}