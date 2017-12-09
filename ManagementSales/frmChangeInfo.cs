using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;

namespace ManagementSales
{
    public partial class frmChangeInfo : Form
    {
        private IChangeInfoBUS changeInfoBUS;
        private string id;
        public frmChangeInfo(IChangeInfoBUS changeInfo)
        {
            InitializeComponent();
            this.changeInfoBUS = changeInfo;
            id = Program.IDStaff;
        }


        /// <summary>
        ///Hiển thị thông báo khi có bất kì <see cref="Exception"/> nào bị phát hiện 
        /// </summary>
        /// <param name="ex"></param>
        void WarningMessageBox(Exception ex)
        {
            MessageBox.Show($"Lỗi trong quá trình thực thi.Mã lỗi :\n {ex.Message.ToString()} \\\n Vui lòng liên hệ người quản trị " +
                $"hoặc nhân viên để được nhận thêm sự " +
                $" hỗ trợ", "Lỗi Trong Quá Trình Thực Thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Cursor = Cursors.Default;
        }



        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {                
                bool result = false;
                if (rdbChangeName.Checked == true)
                {
                    if (txtName.Text.Trim().Length==0||txtReName.Text.Trim().Length==0)
                    {
                        MessageBox.Show("Điền vào chỗ trống","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                    if (!txtName.Text.Equals(txtReName.Text))
                    {
                        MessageBox.Show("Dữ liệu không trùng");
                        return;
                    }
                    result = changeInfoBUS.ChangeInfo(txtName.Text, null, id);
                }
                else if (rdbChangePass.Checked == true)
                {
                    if (txtPass.Text.Trim().Length == 0 || txtRePass.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Điền vào chỗ trống", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!txtPass.Text.Equals(txtRePass.Text))
                    {
                        MessageBox.Show("Dữ liệu không trùng");
                        return;
                    }
                    result = changeInfoBUS.ChangeInfo(null, txtPass.Text, id);
                }
                else if (rdbChangeBoth.Checked == true)
                {

                    if (txtName.Text.Trim().Length == 0 || txtReName.Text.Trim().Length == 0
                        || txtPass.Text.Trim().Length == 0 || txtRePass.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Điền vào chỗ trống", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var CheckName = txtName.Text.Equals(txtReName.Text);
                    var checkPass = txtPass.Text.Equals(txtRePass.Text);
                    if (CheckName==false||checkPass==false)
                    {
                        MessageBox.Show("Dữ liệu không trùng");
                        return;
                    }
                    result = changeInfoBUS.ChangeInfo(txtName.Text, txtPass.Text, id);
                }
                var stringResult = result ? "Thành công" : "Thất bại";
                MessageBox.Show(stringResult,"Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                if (stringResult.Equals("Thành công"))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        private void RdbChangeName_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = txtReName.Enabled = true;
            txtPass.Enabled = txtRePass.Enabled = false;
        }

        private void RdbChangePass_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = txtReName.Enabled = false;
            txtPass.Enabled = txtRePass.Enabled = true;
        }

        private void RdbChangeBoth_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = txtReName.Enabled = true;
            txtPass.Enabled = txtRePass.Enabled = true;
        }
    }
}
