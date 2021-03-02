using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using core.UseCase.ConvertData;
using core.UseCase.DownloadData;

namespace WinApp
{
    public partial class Main : Form
    {
        private List<core.Entities.ConvertData.SapModel> _lst;
        public Main()
        {

            InitializeComponent();
            if (!Program.IsAdmin)
            {
                Users.Visible = false;
                //this.Controls.Remove(button1);
            }
            checkedListBox1.DataSource = Enum.GetValues(typeof(core.Repository.Types.CommerceType));
            //init test
            button3.Enabled = true;
            for (int count = 0; count < checkedListBox1.Items.Count; count++)
            {
                checkedListBox1.SetItemChecked(count, true);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void Main_Closing(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog search = new FolderBrowserDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = search.SelectedPath;
                textBox2.Enabled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnChargeFileInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(search.FileName);
                if (ext != "")
                {
                    const string txtExtension = "El archivo no debe tener extensión";
                    Program.LogInfo(txtExtension);
                    MessageBox.Show(txtExtension);
                }
                else
                {
                    var res = new ChargeFile().ValidatePath(search.FileName, out DateTime dat);
                    if (res != String.Empty)
                    {
                        Program.LogInfo(res);
                        MessageBox.Show(res);
                        textBoxInput.Text = String.Empty;
                        return;

                    }

                    var msj = "La fecha del archivo es " + dat.ToString("yyyy-MM-dd");
                    Program.LogInfo(msj);
                    MessageBox.Show(msj);
                    textBoxInput.Text = search.FileName;
                    textBoxInput.Enabled = true;
                }
            }
        }

        private void textBox_Input(object sender, EventArgs e)
        {

        }

        private void btnCharge_Click(object sender, EventArgs e)
        {

            //_waitForm.Show();
            //loading.BringToFront();
            
            loading.Visible = true;
            loading.Dock = DockStyle.Fill;
            loading.Refresh();
            if (Program.IsAdmin)
                Users.Enabled = false;
            Cursor = Cursors.Arrow;

            try
            {
                button2.Enabled = false;
                //Cursor = Cursors.WaitCursor; // change cursor to hourglass type


                if (textBoxInput.Text != "")
                {
                    var ret = new ChargeFile().Build(textBoxInput.Text);
                    //_waitForm.Close();

                    if (ret.IsTrue)
                    {
                        _lst = ret.List;
                        Program.LogInfo("el archivo " + textBoxInput.Text + " " + ret.Message);
                        MessageBox.Show(ret.Message);
                        button3.Enabled = true;
                    }
                    else
                    {
                        Program.LogInfo(ret.Message);
                        MessageBox.Show(ret.Message);
                    }
                }
                else
                {
                    const string selectFile = "Debe seleccionar un archivo para cargar";
                    //_waitForm.Close();
                    Cursor = Cursors.Arrow;
                    MessageBox.Show(selectFile);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (InvalidOperationException ex)
            {
                var mess =
                    "The instance of entity type 'SapModel' cannot be tracked because another instance with the same key value for";
                var message = ex.Message;
                if (ex.Message.Contains(mess))
                {
                    message = "El Arhivo tiene lineas duplicadas";
                    Program.LogInfo(message);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
                MessageError();
                Program.LogInfo(message);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
                Program.LogError(ex);
                MessageError();
            }
            finally
            {
                loading.Visible = false;
                if (Program.IsAdmin)
                    Users.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Program.IsAdmin)
                Users.Enabled = false;
            loading.Visible = true;
            loading.Dock = DockStyle.Fill;
            loading.BringToFront();
            loading.Refresh();
            //_waitForm.Show();

            Cursor = Cursors.WaitCursor;
            try
            {
                var rute = textBox2.Text;
                List<core.Repository.Types.CommerceType> subfolder =
                    checkedListBox1.CheckedItems.OfType<core.Repository.Types.CommerceType>().ToList();
                if (rute != "")
                {
                    if (subfolder.Count != 0)
                    {
                        button2.Enabled = false;
                        // change cursor to hourglass type
                        var res = new DownloadFiles().Build(rute, subfolder, _lst);
                        if (res)
                        {
                            var txtFine = "Se ha generado con Éxito ";
                            subfolder.ForEach(s => { Program.LogInfo(txtFine + "el archivo " + s); });
                            Cursor = Cursors.Arrow;
                            MessageBox.Show(txtFine);
                        }
                    }
                    else
                    {
                        var selectItem = "Debe seleccionar al menos un item";
                        Program.LogInfo(selectItem + " para generar el archivo");
                        Cursor = Cursors.Arrow;
                        MessageBox.Show(selectItem);
                    }
                }
                else
                {
                    var selectRute = "No a seleccionado una ruta ";
                    Program.LogInfo(selectRute + "para generar el archivo");
                    Cursor = Cursors.Arrow;
                    MessageBox.Show(selectRute);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                button2.Enabled = true;
                Program.LogError(ex);
                Cursor = Cursors.Arrow;
                MessageError();
            }
            finally
            {
                loading.Visible = false;
                if (Program.IsAdmin)
                    Users.Enabled = true;
            }
        }

        private static void MessageError()
        {

            var txtError = "Hubo un error comuniquese con el administrador";
            MessageBox.Show(txtError);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Program.LogInfo("El usuario abre la sección Administrador");
            UserAdmin charge = new UserAdmin();
            charge.Show();
            this.Hide();
        }
    }
}
