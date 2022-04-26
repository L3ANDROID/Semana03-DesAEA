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
    public partial class Persona : Form
    {
        SqlConnection conn;

        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                String sql = "SELECT * FROM Person";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
            }
            else
            {
                MessageBox.Show("La conexión está cerrada");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if( conn.State == ConnectionState.Open)
            {
                try
                {
                    String FirstName = txtNombre.Text;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "BuscarPersonaNombre";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@FirstName";
                    param.SqlDbType = SqlDbType.NVarChar;
                    param.Value = FirstName;

                    cmd.Parameters.Add(param);

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgvListado.DataSource = dt;
                    dgvListado.Refresh();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //conn.Close();
                }
            }
            else
            {
                MessageBox.Show("La conexión está cerrada");
            }
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            List<PersonaModelo> personas = new List<PersonaModelo>();

            if( conn.State == ConnectionState.Open)
            {
                try
                {
                    String FirstName = txtNombre.Text;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "BuscarPersonaNombre";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@FirstName";
                    param.SqlDbType = SqlDbType.NVarChar;
                    param.Value = FirstName;

                    cmd.Parameters.Add(param);

                    //sin dataTable, usando dataSet
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        personas.Add(new PersonaModelo
                        {
                            Id = Convert.ToInt32(reader["PersonID"]),
                            Name = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"])
                        });
                    }

                    dgvListado.DataSource = personas;
                    dgvListado.Refresh();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //conn.Close();
                }
            }
            else
            {
                MessageBox.Show("La conexión está cerrada");
            }
        }
    }
}
