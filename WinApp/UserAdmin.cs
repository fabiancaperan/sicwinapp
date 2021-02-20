using System;
using System.Windows.Forms;
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
            var headerText = "Usuario";
            dataGridView1.Columns[0].HeaderText = headerText;
            var administrador = "Administrador";
            dataGridView1.Columns[1].HeaderText = administrador;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            _ = ((DataGridView)sender).SelectedCells[0].ReadOnly;
            var userName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var isAdmin = Boolean.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() ?? string.Empty);
            textBox1.Text = userName;
            checkBox1.Checked = isAdmin;





        }

        private void button3_Click(object sender, EventArgs e)
        {
            var userName = textBox1.Text.Trim();
            if (_userDb.DeleteUsers(userName))
            {
                var delete = " se ha eliminado correctamente";
                Program.LogInfo(userName + delete);
                MessageBox.Show(userName + delete);
                dataGridView1.DataSource = _userDb.GetUsers();
            }
            else
            {
                var noExist = " no existe como usuario";
                Program.LogInfo(userName + " no existe como usuario para eliminar");
                MessageBox.Show(userName + noExist);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userName = textBox1.Text.Trim();
            var isAdmin = checkBox1.Checked;
            if (_userDb.UpsertUser(userName, isAdmin))
            {
                var created = " se ha creado correctamente";
                Program.LogInfo(userName + created);
                MessageBox.Show(userName + created);
            }
            else
            {
                var msj = " se ha actualizado correctamente";
                Program.LogInfo(userName + msj);
                MessageBox.Show(userName + msj);
            }
            dataGridView1.DataSource = _userDb.GetUsers();
        }

        private void Volver_Click(object sender, EventArgs e)
        {
            Main charge = new Main();
            charge.Show();
            this.Hide();
        }

        private void UserAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void UserAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
