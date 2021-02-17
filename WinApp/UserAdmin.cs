using System;
using System.Collections.Generic;
using System.Windows.Forms;
using core.Entities.UserData;
using core.Repository.Sic.Users;

namespace WinApp
{
    public partial class UserAdmin : Form
    {
        private readonly UserDb _userDb;

        public UserAdmin()
        {
            _userDb = new UserDb();
            InitializeComponent();
            dataGridView1.DataSource = _userDb.GetUsers();
            dataGridView1.Columns[0].HeaderText = "Usuario";
            dataGridView1.Columns[1].HeaderText = "Administrador";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            Boolean readOnly = (sender as DataGridView).SelectedCells[0].ReadOnly;
            var userName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var isAdmin = Boolean.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            textBox1.Text = userName;
            checkBox1.Checked = isAdmin;





        }

        private void button3_Click(object sender, EventArgs e)
        {
            var userName = textBox1.Text.Trim();
            if (_userDb.DeleteUsers(userName))
            {
                Program.logInfo(userName + " se ha eliminado correctamente");
                MessageBox.Show(userName + " se ha eliminado correctamente");
                dataGridView1.DataSource = _userDb.GetUsers();
            }
            else
            {
                Program.logInfo(userName + " no existe como usuario para eliminar");
                MessageBox.Show(userName + " no existe como usuario");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userName = textBox1.Text.Trim();
            var isAdmin = checkBox1.Checked;
            if (_userDb.UpsertUser(userName, isAdmin))
            {
                Program.logInfo(userName + " se ha creado correctamente");
                MessageBox.Show(userName + " se ha creado correctamente");
            }
            else
            {
                Program.logInfo(userName + " se ha actualizado correctamente");
                MessageBox.Show(userName + " se ha actualizado correctamente");
            }
            dataGridView1.DataSource = _userDb.GetUsers();
        }

        private void Volver_Click(object sender, EventArgs e)
        {
            Main charge = new Main();
            charge.Show();
            this.Hide();
        }
    }
}
