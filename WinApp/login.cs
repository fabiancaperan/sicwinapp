﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using core.Repository.Sic.Users;

namespace WinApp
{
    public partial class Login : Form
    {
        private const string ErrorMessage = "Hubo un error comuniquese con el administrador";

        private readonly UserDb _userDb;
        public Login()
        {
            InitializeComponent();
            _userDb = new UserDb();
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
            //_waitForm.Show();
            loading.Visible = true;
            loading.Dock = DockStyle.Fill;
            loading.BringToFront();
            Program.UserName = textuser.Text;

            try
            {
                //throw new InvalidOperationException("Logfile cannot be read-only");
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                var user = _userDb.GetUser(textuser.Text);

                if (user == null)
                {
                    var userNoRegistry = "Usuario no registrado en la aplicación";
                    Program.LogInfo(userNoRegistry);
                    //_waitForm.Close();
                    //Cursor = Cursors.Arrow;
                    MessageBox.Show(userNoRegistry);
                    //return;
                }
                else
                {
                    const string admin = "admin";
                    if (textuser.Text == admin)
                    {
                        if (textpass.Text == admin)
                        {
                            Program.IsAdmin = user.isAdmin;
                            Program.LogInfo("Usuario autenticado");
                            Main charge = new Main();
                            charge.Show();
                            this.Hide();
                        }
                        else
                        {
                            var passNoValid = "Contraseña inválida";
                            Program.LogInfo(passNoValid);
                            MessageBox.Show(passNoValid);
                        }
                    }

                    else
                    {
                        string login = "LOGIN";

                        var res = new ValidateLdap().Validate(textuser.Text, textpass.Text);
                        //_waitForm.Close();
                        Cursor = Cursors.Arrow;
                        MessageBox.Show(res);
                        if (login != res)
                        {
                            Program.LogInfo(res);
                            return;
                        }

                        Program.IsAdmin = user.isAdmin;
                        Program.LogInfo("Usuario autenticado");
                        Main charge = new Main();
                        charge.Show();
                        this.Hide();
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                //_waitForm.Close();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                Program.LogError(ex);
                //_waitForm.Close();
                Cursor = Cursors.Arrow;
                MessageError(ex);
            }
            finally
            {
                loading.Visible = false;
            }
        }

        private static void MessageError(Exception ex)
        {
            Console.WriteLine(ex.Message);
            MessageBox.Show(ErrorMessage);
        }

    }
}