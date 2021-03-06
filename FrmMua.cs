﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuaHangGiayDep
{
    public partial class FrmMua : Form
    {
        DataTable tblMua;
        public FrmMua()
        {
            InitializeComponent();
        }

        private void FrmMua_Load(object sender, EventArgs e)
        {
            txtMaMua.Enabled = false;
            loadDataToGridview();
        }
        private void loadDataToGridview()
        {
            string sql = "select * from Mua";
            tblMua = Functions.GetDataToTable(sql);

            dataGridView_Mua.DataSource = tblMua;

        }
        private void ResetValue()
        {
            txtMaMua.Text = "";
            txtTenMua.Text = "";
        }

        private void dataGridView_Mua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaMua.Text = dataGridView_Mua.CurrentRow.Cells["MaMua"].Value.ToString();
            txtTenMua.Text = dataGridView_Mua.CurrentRow.Cells["TenMua"].Value.ToString();
            txtMaMua.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            btn_sua.Enabled = false;
            btn_xoa.Enabled = false;
            btn_huy.Enabled = true;
            btn_them.Enabled = false;
            ResetValue();
            txtMaMua.Enabled = true;
            txtMaMua.Focus();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMua.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if (txtMaMua.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã mùa nào", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMaMua.Focus();
            }
            if (txtTenMua.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên mùa", " Thông báo",

                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtTenMua.Focus();

            }
            sql = " UPDATE Mua SET TenMua =  '" + txtTenMua.Text.ToString() + "' WHERE MaMua='" + txtMaMua.Text + "'";
            Functions.RunSqlDel(sql);
            ResetValue();
            loadDataToGridview();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMua.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if (txtMaMua.Text == "")
            {
                MessageBox.Show("ban chua chon ma mua");
            }
            if (MessageBox.Show("bạn có muốn xóa không?", "thong bao",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete from Mua where MaMua='" + txtMaMua.Text + "'";
                Functions.RunSqlDel(sql);
                loadDataToGridview();
                ResetValue();
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMua.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu");
                return;
            }
            if (txtMaMua.Text == "")
            {
                MessageBox.Show("nhap ma mùa");
                txtMaMua.Focus();

            }
            if (txtTenMua.Text == "")
            {
                MessageBox.Show("nhap ten mùa");
                txtTenMua.Focus();
            }

            sql = "select MaMua from Mua where MaMua='" + txtMaMua.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mùa này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMua.Focus();

                return;
            }
            sql = "insert into Mua Values('" + txtMaMua.Text + "','" + txtTenMua.Text + "')";
            Functions.RunSqlDel(sql);
            loadDataToGridview();
            ResetValue();
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            ResetValue();
            btn_huy.Enabled = false;
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            txtMaMua.Enabled = false;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có chắc chắn muốn thoát chương trình không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
