﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using School.Core;

namespace School.Droid
{
	public class LichHocTuanAdapter: BaseExpandableListAdapter
	{

		public List<chiTietLH> listCT;
		private Context context;

		public LichHocTuanAdapter (Context context, List<chiTietLH> listCT)
		{
			this.context = context;
			this.listCT = listCT;
		}



		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{

			View header = convertView;
			if (header == null) {
				header = LayoutInflater.From (context).Inflate (Resource.Layout.LHTuanHeader, null);
			}
			LichHoc lh = BLichHoc.GetLH (SQLite_Android.GetConnection (), listCT [groupPosition].Id);
			MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), lh.MaMH);
			header.FindViewById<TextView> (Resource.Id.txtMon_Tuan).Text ="Tên môn học: " + mh.TenMH ;
			header.FindViewById<TextView> (Resource.Id.txtThu_Tuan).Text = listCT [groupPosition].Thu;
			header.FindViewById<TextView> (Resource.Id.txtTietBD_Tuan).Text = "Tiết bắt đầu: " +listCT [groupPosition].TietBatDau;
			header.FindViewById<TextView> (Resource.Id.txtSoTiet_Tuan).Text = "Số tiết: " +listCT [groupPosition].SoTiet;
			header.FindViewById<TextView> (Resource.Id.txtPhong_Tuan).Text = "Phòng: " +listCT [groupPosition].Phong;
			return header;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if (row == null) {
				row = LayoutInflater.From (context).Inflate (Resource.Layout.LHTuanRow, null);
			}
			if (childPosition == 0) {
				LichHoc lh = BLichHoc.GetLH (SQLite_Android.GetConnection (), listCT [groupPosition].Id);
				MonHoc mh = BMonHoc.GetMH (SQLite_Android.GetConnection (), lh.MaMH);
				row.FindViewById<TextView> (Resource.Id.txtCBGD).Text = "Giảng viên: " + listCT [groupPosition].CBGD;
				row.FindViewById<TextView> (Resource.Id.txtMaMH).Text = "Mã môn học: " + lh.MaMH;
				row.FindViewById<TextView> (Resource.Id.txtSoTC).Text = "Số tín chỉ: " + mh.SoTC;
				row.FindViewById<TextView> (Resource.Id.txtNMH).Text = "Nhóm môn học: "+lh.NhomMH;
				row.FindViewById<TextView> (Resource.Id.txtMaLop).Text = "Mã lớp: "+lh.MaLop;
				row.FindViewById<TextView> (Resource.Id.txtThoiGian).Text = "Thời gian: "+ listCT [groupPosition].ThoigianBD + " -> " + listCT [groupPosition].ThoigianKT;

			}

			return row;
		}

		public override int GroupCount {
			get {
				return listCT.Count;;
			}
		}

		public override int GetChildrenCount (int groupPosition)
		{
			return 1;
		}

		#region implemented abstract members of BaseExpandableListAdapter

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return false;
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

		#endregion
	}
}

