using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blocc_de__la_nota_wacho
{
    public partial class Form1 : Form
    {
        private string rutaArchivoActual;
        private bool hayCambiosSinGuardar;

        public Form1()
        {
            InitializeComponent();
            richTextBox1.TextChanged += richTextBox1_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            hayCambiosSinGuardar = true;
        }

        private bool PuedeDescartarCambios()
        {
            if (!hayCambiosSinGuardar)
            {
                return true;
            }

            DialogResult respuesta = MessageBox.Show(
                "Hay cambios sin guardar. ¿Quieres guardarlos?",
                "Cambios sin guardar",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Cancel)
            {
                return false;
            }

            return respuesta == DialogResult.No || GuardarArchivo();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PuedeDescartarCambios())
            {
                return;
            }

            richTextBox1.Clear();
            rutaArchivoActual = null;
            hayCambiosSinGuardar = false;
            Text = "Bloc de notas - Nueva nota";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PuedeDescartarCambios())
            {
                return;
            }

            using (OpenFileDialog ventanaAbrir = new OpenFileDialog())
            {
                ventanaAbrir.Title = "Abrir nota";
                ventanaAbrir.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";

                if (ventanaAbrir.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    richTextBox1.Text = File.ReadAllText(ventanaAbrir.FileName);
                    rutaArchivoActual = ventanaAbrir.FileName;
                    hayCambiosSinGuardar = false;
                    Text = "Bloc de notas - " + Path.GetFileName(rutaArchivoActual);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo abrir el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool GuardarArchivo()
        {
            if (string.IsNullOrEmpty(rutaArchivoActual))
            {
                return GuardarComoArchivo();
            }

            try
            {
                File.WriteAllText(rutaArchivoActual, richTextBox1.Text);
                hayCambiosSinGuardar = false;
                Text = "Bloc de notas - " + Path.GetFileName(rutaArchivoActual);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool GuardarComoArchivo()
        {
            using (SaveFileDialog ventanaGuardar = new SaveFileDialog())
            {
                ventanaGuardar.Title = "Guardar nota";
                ventanaGuardar.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                ventanaGuardar.DefaultExt = "txt";
                ventanaGuardar.AddExtension = true;

                if (ventanaGuardar.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                rutaArchivoActual = ventanaGuardar.FileName;
                return GuardarArchivo();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarComoArchivo();
        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Quieres eliminar el contenido de la nota?",
                "Eliminar nota",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                richTextBox1.Clear();
                rutaArchivoActual = null;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PuedeDescartarCambios())
            {
                Close();
            }
        }


        private void CambiarColor_Click(object sender, EventArgs e)
        {
            // Convertimos el 'sender' en un botón para poder cambiarle el color
            Button miBoton = (Button)sender;

            // Evaluamos si el fondo es blanco. Si es blanco, pasamos a Oscuro.
            if (richTextBox1.BackColor == Color.White || richTextBox1.BackColor == Color.WhiteSmoke)
            {
                // ------------------ PASAR A MODO OSCURO ------------------
                Color fondoOscuroTotal = Color.FromArgb(30, 30, 30);
                Color grisComponentes = Color.FromArgb(45, 45, 48);

                this.BackColor = fondoOscuroTotal;
                richTextBox1.BackColor = fondoOscuroTotal;
                richTextBox1.ForeColor = Color.White;

                // Pintamos la barra de menú de arriba
                menuStrip1.BackColor = grisComponentes;
                menuStrip1.ForeColor = Color.White;

                // Pintamos el botón usando la variable que creamos arriba
                miBoton.BackColor = grisComponentes;
                miBoton.ForeColor = Color.White;
            }
            else
            {
                // ------------------ PASAR A MODO CLARO ------------------
                this.BackColor = Color.White;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;

                // Devolvemos la barra de menú a su estado claro
                menuStrip1.BackColor = Color.WhiteSmoke;
                menuStrip1.ForeColor = Color.Black;

                // Devolvemos el botón a su estado claro
                miBoton.BackColor = Color.LightGray;
                miBoton.ForeColor = Color.Black;
            }
        }

     
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivo();
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

