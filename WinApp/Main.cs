using core.UseCase.ConvertData;
using core.UseCase.DownloadData;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace WinApp
{
    public partial class Main : Form
    {
        private List<core.Entities.ConvertData.SapModel> lst;
        public Main()
        {
            
            InitializeComponent();
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
                    MessageBox.Show(txtExtension);
                }
                else
                {
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
            try
            {
                button2.Enabled = false;
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type


                if (textBoxInput.Text != "")
                {
                    var ret = new ChargeFile().Build(textBoxInput.Text);
                    if (ret.Message == "TRUE")
                    {
                        lst = ret.List;
                        var txtCheck = "Se ha cargado correctamente";
                        MessageBox.Show(txtCheck);
                        button3.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(ret.Message);
                    }
                }
                else
                {
                    const string selectFile = "Debe seleccionar un archivo para cargar";
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
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
                MessageError(message);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
                MessageError(ex.Message);
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
             try
            {
                var rute = textBox2.Text;
                List<core.Repository.Types.CommerceType> subfolder = checkedListBox1.CheckedItems.OfType<core.Repository.Types.CommerceType>().ToList();
                if (rute != "")
                {
                    if (subfolder.Count() != 0)
                    {
                        button2.Enabled = false;
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        var res = new DownloadFiles().Build(rute, subfolder, lst);
                        if (res)
                        {
                            var txtFine = "Se ha generado con Éxito";
                            MessageBox.Show(txtFine);
                        }
                    }
                    else
                    {
                        var selectItem = "Debe seleccionar al menos un item";
                        MessageBox.Show(selectItem);
                    }
                }
                else
                {
                    var selectRute = "No a seleccionado una ruta";
                    MessageBox.Show(selectRute);
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                button2.Enabled = true;
                Cursor = Cursors.Arrow; // change cursor to normal type
                MessageError(ex.Message);
            }
        }

        private static void MessageError(string message)
        {
            
            //var txtError = "Hubo un error comuniquese con el administrador";
            MessageBox.Show(message);
        }
    }
}
