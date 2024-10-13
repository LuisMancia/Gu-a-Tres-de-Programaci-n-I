using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10112024_Tarea3PrograI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Presentacion de las Variables
        String nombre = string.Empty;
        double notaParcial, notaGuia, notaCtrlLectura; //aqui se almacenan los valores de las notas
        double porcentajeParcial = 0.60, porcentajeGuia, porcentajeCtrlLectura; // aqui se almacena el peso de la nota en el promedio final
        double resultado;

        //Almacenamiento del contenido ingresado en el textbox etiquetado como "Nombre"
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            nombre = txtNombre.Text;
        }

        //Este metodo deshabilita los campos del formulario Control de Lectura
        private void deshabilitarCampos()
        {
            txtCtrlLectura.Enabled = false;
            poderacionCtrlLectura.Enabled = false;
        }

        //Este metodo habilita los campos del formulario Control de Lectura
        private void habilitarCampos()
        {
            txtCtrlLectura.Enabled = true;
            poderacionCtrlLectura.Enabled = true;
        }
        
        //Este metodo inhabilita los campos del formulario del control de lectura 
        //y asigna valores por default en caso de estar deshabilitado para que el
        //codigo no tome valores que se escriban en el formulario si este se encuentra
        //apagado.
        private void poderacionGuia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(poderacionGuia.SelectedIndex == 1)
            {
                deshabilitarCampos();
                //si el porcentaje seleccionado es 40 % entonces el siguiente
                //formulario se va a deshabilitar.
                porcentajeGuia = 0.40;
            }
            else
            {
                habilitarCampos();
                //si la guia vale 20 % entonces si se habilita el ingreso de nota del control de lectura
                porcentajeGuia = 0.20;
            }
        }

        //En este metodo se calcula el porcentage de nota segun lo que se haya seleccionado en el combobox
        private void poderacionCtrlLectura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtCtrlLectura.Enabled)
            {
                porcentajeCtrlLectura = 0.20;
            } 
            else
            {
                notaCtrlLectura = 0; // si el campo para ingresar la nota del control de lectura no esta habilitad
                //entonces por default la nota seria cero, para no afectar el promedio final.
                porcentajeCtrlLectura = 0;
                txtCtrlLectura.Clear();
            }
        }

        //Almacenamiento del contenido ingresado en el textbox etiquetado como "Parcial"
        private void txtParcial_TextChanged(object sender, EventArgs e)
        {
            bool bandera = double.TryParse(txtParcial.Text, out notaParcial);
            if (notaParcial >= 0 && notaParcial <=10 ) { do
                {
                    if (bandera)
                    {
                        mensajeError1.Clear();
                        bandera = false;
                    }
                    else
                    {
                        mensajeError1.SetError(txtParcial, "Solo se permiten numeros.");
                    }
                } while (bandera);
            }
            else
            {
                mensajeError1.SetError(txtParcial, "El valor es invalido.");
            }
        }

        //Almacenamiento del contenido ingresado en el textbox etiquetado como "Guia"
        private void txtGuia_TextChanged(object sender, EventArgs e)
        {
            bool bandera1 = double.TryParse(txtGuia.Text, out notaGuia);
            if (notaGuia>=0 && notaGuia<=10) {
                do
                {
                    if (bandera1)
                    {
                        mensajeError2.Clear();
                        bandera1 = false;
                    }
                    else
                    {
                        mensajeError2.SetError(txtGuia, "Solo se permiten numeros");
                    }
                } while (bandera1);
            }
            else
            {
                mensajeError2.SetError(txtGuia, "El valor es invalido.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogoResultado1 = MessageBox.Show("Desarrollado por " +"\n"+"Br. Luis Fernando Mancia Lopez"+"\n"+"Carnet: 27-3442-2023", "Acerca de mi", MessageBoxButtons.OK);
            if (dialogoResultado1 == DialogResult.OK)
            {
                Limpieza();
            }
        }

        //Almacenamiento del contenido ingresado en el textbox etiquetado como "Control de Lectura"
        private void txtCtrlLectura_TextChanged(object sender, EventArgs e)
        {
            if (txtCtrlLectura.Enabled) {
                bool bandera2 = double.TryParse(txtCtrlLectura.Text, out notaCtrlLectura);
                if (notaCtrlLectura >= 0 && notaCtrlLectura <= 10) {
                    do
                    {
                        if (bandera2)
                        {
                            mensajeError3.Clear();
                            bandera2 = false;
                        }
                        else
                        {
                            mensajeError3.SetError(txtCtrlLectura, "Solo se permiten numeros");
                        }
                    } while (bandera2);
                }
                else
                {
                    mensajeError3.SetError(txtCtrlLectura, "El valor es invalido.");
                }
            } else
            {
                txtCtrlLectura.Text = string.Empty;
                notaCtrlLectura = 0;
            }
        }

        private void Limpieza()
        {
            txtNombre.Clear();
            txtParcial.Clear();
            txtGuia.Clear();
            txtCtrlLectura.Clear();
            poderacionCtrlLectura.SelectedIndex = -1;
            poderacionGuia.SelectedIndex = -1;
            txtCtrlLectura.Enabled = true;
            mensajeError1.Clear();
            mensajeError2.Clear();
            mensajeError3.Clear();
            mensajeError4.Clear();
            mensajeError5.Clear();
            mensajeError5.Clear();
        }
        
        private void mensajeBoton()
        {
            resultado = notaParcial * porcentajeParcial + notaGuia * porcentajeGuia + notaCtrlLectura * porcentajeCtrlLectura;
            DialogResult dialogoResultado = MessageBox.Show("Promedio: " + resultado + "\n" + "Nombre: " + nombre, "RESULTADOS", MessageBoxButtons.OK);
            if (dialogoResultado == DialogResult.OK)
            {
                Limpieza();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //El calculo del promedio solo se puede realizar si ya se selecciono el porcentage de la guia
            //y en caso que la guia no tenga el 40 % entonces se verifica que el campo de nota y el
            //porcentaje del control de lectura esten habilitados para hacer el calculo correspondiente.
            //En caso que el campo de ingreso de nota del control de lectura este inhabilitado entonces
            //se manda a llamar al metodo que genera el mensaje en pantalla.
            if (poderacionGuia.SelectedIndex != -1) 
            {
                mensajeError4.Clear();
                if (txtCtrlLectura.Enabled)
                {
                    if(poderacionCtrlLectura.SelectedIndex != -1)
                    {
                        mensajeError5.Clear();
                        mensajeBoton();
                    }
                    else
                    {
                        mensajeError5.SetError(poderacionCtrlLectura, "Seleccione el porcentaje de la nota");
                    }
                }
                else
                {
                    mensajeBoton();
                }
            }
            else
            {
                mensajeError4.SetError(poderacionGuia, "Seleccione el porcentaje de la nota");
            }
        }
    }
}
