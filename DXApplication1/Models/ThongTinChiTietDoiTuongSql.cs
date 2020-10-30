﻿using DXApplication1.Utilizes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DXApplication1.Models
{
    public class ThongTinChiTietDoiTuongSql : ConnectionDatabase
    {
        enum DoiTuong
        {
            MaDoiTuong,
            MaDonVi,
            MoTa,
            ToaDoX,
            ToaDoY,
            MaKeHoach,
            ChieuRongAnh,
            ChieuDaiAnh
        }

        public void AddDoiTuong(List<ThongTinChiTietDoiTuong> list)
        {
            Connection.Open();
            foreach (var doiTuong in list)
            {
                SqlCommand command = new SqlCommand("ThemDoiTuongVaoKeHoach", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@maDonVi", doiTuong.MaDonVi);
                if (doiTuong.MoTa == null)
                {
                    command.Parameters.AddWithValue("@moTa", string.Empty);
                }
                else
                {
                    command.Parameters.AddWithValue("@moTa", doiTuong.MoTa);
                }
                command.Parameters.AddWithValue("@toaDoX", doiTuong.ToaDoX);
                command.Parameters.AddWithValue("@toaDoY", doiTuong.ToaDoY);
                command.Parameters.AddWithValue("@maKeHoach", doiTuong.MaKeHoach);
                command.Parameters.AddWithValue("@chieuRongAnh", doiTuong.ChieuRongAnh);
                command.Parameters.AddWithValue("@chieuDaiAnh", doiTuong.ChieuDaiAnh);
                command.ExecuteScalar();
            }
            Connection.Close();
        }

        public List<ThongTinChiTietDoiTuong> LayCacThongTinDoiTuongTuKeHoach(int MaKeHoach)
        {
            List<ThongTinChiTietDoiTuong> doiTuongs = new List<ThongTinChiTietDoiTuong>();
            Connection.Open();
            SqlCommand command = new SqlCommand("LayDoiTuongTuKeHoach", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@maKeHoach", MaKeHoach);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow doiTuong in table.Rows)
                {
                    doiTuongs.Add(new ThongTinChiTietDoiTuong()
                    {
                        MaKeHoach = doiTuong.Field<int>((int)DoiTuong.MaKeHoach),
                        MoTa = doiTuong.Field<string>((int)DoiTuong.MoTa),
                        MaDonVi = doiTuong.Field<string>((int)DoiTuong.MaDonVi),
                        ToaDoX = doiTuong.Field<int>((int)DoiTuong.ToaDoX),
                        ToaDoY = doiTuong.Field<int>((int)DoiTuong.ToaDoY),
                        ChieuDaiAnh = doiTuong.Field<int>((int)DoiTuong.ChieuDaiAnh),
                        ChieuRongAnh = doiTuong.Field<int>((int)DoiTuong.ChieuRongAnh)
                    });
                }
            }
            Connection.Close();
            return doiTuongs;
        }

        public List<Models.DoiTuong> LayCacDoiTuongTuKeHoach(int MaKeHoach, TreeView treeView, ImageList imageList, IntClass count)
        {
            List<Models.DoiTuong> doiTuongs = new List<Models.DoiTuong>();
            Connection.Open();
            SqlCommand command = new SqlCommand("LayDoiTuongTuKeHoach", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@maKeHoach", MaKeHoach);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                count.IntVar = table.Rows.Count;
                foreach (DataRow doiTuong in table.Rows)
                {
                    int maDoiTuong = doiTuong.Field<int>((int)DoiTuong.MaDoiTuong);
                    int maKeHoach = doiTuong.Field<int>((int)DoiTuong.MaKeHoach);
                    string moTa = doiTuong.Field<string>((int)DoiTuong.MoTa);
                    string maDonVi = doiTuong.Field<string>((int)DoiTuong.MaDonVi);
                    int toaDoX = doiTuong.Field<int>((int)DoiTuong.ToaDoX);
                    int toaDoY = doiTuong.Field<int>((int)DoiTuong.ToaDoY);
                    int chieuDaiAnh = doiTuong.Field<int>((int)DoiTuong.ChieuDaiAnh);
                    int chieuRongAnh = doiTuong.Field<int>((int)DoiTuong.ChieuRongAnh);
                    Models.DoiTuong anh = new Models.DoiTuong()
                    {
                        ThongTinChiTietDoiTuong = new ThongTinChiTietDoiTuong()
                        {
                            MaDoiTuong = maDoiTuong,
                            ChieuDaiAnh = chieuDaiAnh,
                            ChieuRongAnh = chieuRongAnh,
                            ToaDoX = toaDoX,
                            ToaDoY = toaDoY,
                            MaDonVi = maDonVi,
                            MaKeHoach = maKeHoach,
                            MoTa = moTa,
                        }
                    };
                    anh.Picture.Image = imageList.Images[maDonVi];
                    doiTuongs.Add(anh);
                }
            }
            Connection.Close();
            return doiTuongs;
        }


        public void XoaDoiTuong(List<Models.DoiTuong> doiTuongs)
        {
            Connection.Open();
            foreach (Models.DoiTuong doiTuong in doiTuongs)
            {
                SqlCommand command = new SqlCommand("XoaDoiTuong", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@maDoiTuong", doiTuong.ThongTinChiTietDoiTuong.MaDoiTuong);
                command.ExecuteScalar();
            }
            Connection.Close();
        }


        public void SuaDoiTuong(List<Models.DoiTuong> doiTuongs)
        {
            Connection.Open();
            foreach (Models.DoiTuong doiTuong in doiTuongs)
            {
                SqlCommand command = new SqlCommand("CapNhatThongTinDoiTuong", Connection);
                command.CommandType = CommandType.StoredProcedure;
                //if (doiTuong.MoTa == null)
                //{
                    command.Parameters.AddWithValue("@moTa", string.Empty);
                //}
                //else
                //{
                    //command.Parameters.AddWithValue("@moTa", doiTuong.MoTa);
                //}
                command.Parameters.AddWithValue("@toaDoX", doiTuong.ThongTinChiTietDoiTuong.ToaDoX);
                command.Parameters.AddWithValue("@toaDoY", doiTuong.ThongTinChiTietDoiTuong.ToaDoY);
                command.Parameters.AddWithValue("@chieuRongAnh", doiTuong.ThongTinChiTietDoiTuong.ChieuRongAnh);
                command.Parameters.AddWithValue("@chieuDaiAnh", doiTuong.ThongTinChiTietDoiTuong.ChieuDaiAnh);
                command.Parameters.AddWithValue("@maDoiTuong", doiTuong.ThongTinChiTietDoiTuong.MaDoiTuong);
                command.ExecuteScalar();
            }
            Connection.Close();
        }

        public int LayMaDoiTuongLonNhat()
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT MAX(MaDoiTuong) FROM dbo.DoiTuong", Connection);
            sqlCommand.CommandType = CommandType.Text;
            object result = sqlCommand.ExecuteScalar();
            int maxMa = Convert.ToInt32(result);
            Connection.Close();
            return maxMa;

        }
    }


}