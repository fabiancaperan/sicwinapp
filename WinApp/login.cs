using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WinApp
{
    public partial class Login : Form
    {
        private const string ErrorMessage = "Hubo un error comuniquese con el administrador";

        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void textuser_Enter(object sender, EventArgs e)
        {
            string usuario = "USUARIO";
            if (textuser.Text == usuario)
            {
                textuser.Text = "";
                textuser.ForeColor = Color.Black;
            }
        }

        private void textuser_Leave(object sender, EventArgs e)
        {
            if (textuser.Text == "")
            {
                const string usuario = "USUARIO";
                textuser.Text = usuario;
                textuser.ForeColor = Color.Black;
            }
        }

        private void textpass_Enter(object sender, EventArgs e)
        {
            const string contraseña = @"CONTRASEÑA";
            if (textpass.Text == contraseña)
            {
                textpass.Text = "";
                textpass.ForeColor = Color.Black;
                textpass.UseSystemPasswordChar = true;
            }
        }

        private void textpass_Leave(object sender, EventArgs e)
        {
            if (textpass.Text == "")
            {
                var textpassText = "CONTRASEÑA";
                textpass.Text = textpassText;
                textpass.ForeColor = Color.Black;
                textpass.UseSystemPasswordChar = false;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                const string admin = "admin";
                if (textuser.Text == admin)
                {
                    if (textpass.Text == admin)
                    {
                        Main charge = new Main();
                        charge.Show();
                        this.Hide();
                    }
                    else
                    {
                        textpass.Clear();
                        textuser.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageError(ex);
            }
        }

        private static void MessageError(Exception ex)
        {
            Console.WriteLine(ex.Message);
            MessageBox.Show(ErrorMessage);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string login = "LOGIN";
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            var res = new ValidateLdap().Validate(textuser.Text, textpass.Text);
            Cursor = Cursors.Arrow; // change cursor to normal type
            MessageBox.Show(res);
            if (login != res) return;
            Main charge = new Main();
            charge.Show();
            this.Hide();


        }
    }
}
