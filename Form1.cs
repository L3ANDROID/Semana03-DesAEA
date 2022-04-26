using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//Ado .net

namespace Laboratorio03
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //declaramos variables para almacenar los valores de los textBox y definimos la cadena de conexion
            String servidor = txtServidor.Text;
            String bd = txtBaseDatos.Text;
            String user = txtUsuario.Text;
            String pwd = txtPassword.Text;

            String str = "Server=" + servidor + ";Database=" + bd + ";";

            //la cadena de conexion cambian segun el estado del checkbox
            if (chkAutentication.Checked)
                str += "Integrated Security=true";
            else
                str += "User Id="+ user +";Password="+ pwd +";";

            //Abrir una conexion con el servidor, usando la cadena de conexion
            try
            {
                conn = new SqlConnection(str);
                conn.Open();
                MessageBox.Show("Conectado exitosamente");
                btnDesconectar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar al servidor: \n"+ ex.ToString());
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            //intentemos obtener el estado de la conexion, y en caso de que esté abierta, recuperar informacion de la misma
            try
            {
                if (conn.State == ConnectionState.Open)
                    MessageBox.Show("Estado del servidor: " + conn.State +
                        "\nVersion del servidor: " + conn.ServerVersion +
                        "\nBase de datos: " + conn.Database);
                else
                    MessageBox.Show("Estado del servidor: " + conn.State);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imposible determinar el estado del servidor: \n"+
                    ex.ToString());
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            //para cerrar la conexion verificamos que no esté cerrada
            try
            {
                if(conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    MessageBox.Show("Conexion cerrada satisfactoriamente");
                }
                else
                {
                    MessageBox.Show("La conexion ya está cerrada");
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cerrar la conexión: \n"+
                    ex.ToString());
            }
        }

        private void chkAutentication_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutentication.Checked)
            {
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsuario.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnPersonas_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona(conn);
            persona.Show();
        }
    }
}
