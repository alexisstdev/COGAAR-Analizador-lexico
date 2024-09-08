using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace COGAAR
{
    public partial class Form1 : Form
    {
        SqlConnection miConexion = new SqlConnection("server=DESKTOP-OTMQ66M;database=MatrizTransicionCOGAAR; integrated security=true");
        
        char[] caracteres = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                      'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                      '_','0','1','2','3','4','5','6','7','8','9','+','-','/','*','&','|','?','=','>','<','(',')','[',']',';',
                      ',','.','@','$','%','#','`','~','{','}','!','¡','"'
        };

        int _intNuevaFila = 0;
        int numeroLineas = 0;
        int _intNumeroIdentificador = 0;
        int _intCantidadErrores = 0;
        int _intIndiceError = 0;

        bool _blnIden = false;
        bool _blnIdenRepetido = false;
        bool _blnPosibleComentarioCadena = false;
        bool _blnComentarioCadena = false;
        bool _blnTab = false;

        string _strIdentificador = "";
        string _strTab = "";

        List<string> lineas = new List<string>();
        List<string> misIdentificadores = new List<string>();
        List<int> misNumerosIdentificadores = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //Stopwatch timeMeasure = new Stopwatch();
            //timeMeasure.Start();

            if (txtPrograma.Text == string.Empty)
            {
                MessageBox.Show("El programa esta vacio");
                return;
            }

            txtPrograma.Enabled = false;
            btnHabilitar.Enabled = true;
            btnGuardarArchivo.Enabled = true;

            lineas.Clear();
            dgvErroresLexicos.Rows.Clear();
            dgvTablaSimbolos.Rows.Clear();
            misIdentificadores.Clear();
            misNumerosIdentificadores.Clear();

            _blnIdenRepetido = false;

            _strTokens = "";
            _strTab = "";

            _intLineaError = 1;
            _intNumeroIdentificador = 0;
            _intCantidadErrores = 0;

            // Dividir el contenido del TextBox en líneas
            string[] lineasTextBox = txtPrograma.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Agregar cada línea a la lista
            foreach (string linea in lineasTextBox)
            {
                lineas.Add(linea);
            }

            foreach (string miOperacion in lineas) //Ciclo para recorrer la lista de operaciones
            {
                _blnTab = true;
                foreach (char miCaracter in miOperacion) //Ciclo para recorrer los caracteres de cada palabra de la lista 
                {
                    //TABS
                    if (_blnTab && miCaracter == ' ')
                    {
                        _strTab += miCaracter;

                        if (_strTab == "     ") //Si detecta 5 espacios, muestra el token TAB
                        {
                            MostrarToken("TAB", true, "");
                            _strTab = "";
                        }
                    }
                    else //No TABS, encontro un caracter del alfabeto, es decir, ya no puede realizar tabs
                    {
                        _blnTab = false;

                        //Si es un comentario o cadena, ignora los espacios en blanco (no lo toma como otro lexema)
                        if (_blnComentarioCadena && miCaracter == ' ')
                        {
                            //(No hace nada)
                        }
                        else
                        {
                            if (miCaracter == ' ')//Verifica si termina el primer lexema
                            {
                                Consultar(false, ' ', true);
                            }
                            else if (CalcularNumeroColumna(miCaracter) != -1)//¿Es un caracter que pertenece al alfabeto?
                            {
                                Consultar(true, miCaracter, true);

                                if (miCaracter == '"' || miCaracter == '/')
                                {
                                    _blnPosibleComentarioCadena = true;
                                }
                                if (miCaracter != ' ' && _blnPosibleComentarioCadena || miCaracter == '/' && _blnPosibleComentarioCadena)
                                {
                                    _blnComentarioCadena = true;
                                }
                                if (_blnComentarioCadena && miCaracter == '"')
                                {
                                    _blnComentarioCadena = false;
                                }
                            }
                            else
                            {
                                _intNuevaFila = 188;
                            }
                        }
                    }
                }
                Consultar(false, ' ', false);
                _blnPosibleComentarioCadena = false;
                _blnComentarioCadena = false;
            }
            textBox2.Text = textBox1.Text;
            lblCantidadErrores.Text = "Cantidad de errores: " + _intCantidadErrores;

            if (_intCantidadErrores > 0)
            {
                // Itera sobre cada coincidencia de la palabra "errores" en el texto.
                foreach (Match match in Regex.Matches(txtTokens.Text, @"\bError\b", RegexOptions.IgnoreCase))
                {
                    // Cambia el color de la palabra "errores" a rojo.
                    txtTokens.Select(match.Index, match.Length);
                    txtTokens.SelectionColor = Color.Red;
                }

                // Restaura el color predeterminado del texto.
                txtTokens.Select(0, 0);
                txtTokens.SelectionColor = txtTokens.ForeColor;
            }

            //timeMeasure.Stop();
            //MessageBox.Show($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds} ms");
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Abre un cuadro de diálogo para que el usuario seleccione un archivo de texto
            OpenFileDialog miArchivoTxt = new OpenFileDialog();
            miArchivoTxt.Filter = "Archivos de texto (*.txt)|*.txt";
            miArchivoTxt.FilterIndex = 1;

            if (miArchivoTxt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lee el contenido del archivo seleccionado
                    string _strArchivoTxt = File.ReadAllText(miArchivoTxt.FileName);

                    // Ingresa el contenido en el TextBox
                    txtPrograma.Text = _strArchivoTxt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al leer el archivo: " + ex.Message);
                }
            }
        }

        int CalcularNumeroColumna(char _cCaracter)
        {
            for (int i = 0; i < caracteres.Length; i++)
            {
                if (caracteres[i] == _cCaracter)
                {
                    return i;
                }
            }
            return -1;
        }

        string _strTokens = "";
        int _intLineaError = 1;

        public void Consultar(bool _blnNuevaFila, char _cCaracter, bool _blnMismaLinea)
        {
            miConexion.Open();
            if (_blnNuevaFila)
            {
                //Checa si es un identificador (para mandarlo a la tabla de simbolos)
                if (_cCaracter == '_')
                {
                    _blnIden = true;
                }

                if (_blnIden)
                {
                    _strIdentificador += _cCaracter;
                }

                SqlCommand miNuevaFila = new SqlCommand("SELECT COLUMN" + (CalcularNumeroColumna(_cCaracter) + 4) +
                                                       " FROM [Matriz para SQL] " +
                                                       "WHERE COLUMN1 = " + _intNuevaFila, miConexion);
                SqlDataReader miLector = miNuevaFila.ExecuteReader();
                miLector.Read();
                _intNuevaFila = int.Parse(miLector[0].ToString());
                miLector.Close();
            }
            else //FDC
            {
                for (int i = 0; i < 2; i++)
                {
                    SqlCommand miFDC = new SqlCommand("SELECT COLUMN" + (i + 2) +
                                                               " FROM [Matriz para SQL] " +
                                                               "WHERE COLUMN1 = " + _intNuevaFila, miConexion);
                    SqlDataReader miLector = miFDC.ExecuteReader();
                    miLector.Read();
                    if (i == 0)
                    {
                        _intNuevaFila = int.Parse(miLector[0].ToString());
                    }
                    else
                    {
                        MostrarToken(miLector[0].ToString(), _blnMismaLinea, _strIdentificador);
                    }
                    miLector.Close();
                }
                _intNuevaFila = 0;
                _blnIden = false;
                _strIdentificador = "";
            }
            miConexion.Close();
        }

        void MostrarToken(string _strToken, bool _blnMismaLinea, string _strIden)
        {
            if (int.TryParse(_strToken, out _)) //Checa si el token es un numero (Un error)
            {
                MostrarErrorLexico(int.Parse(_strToken), _intLineaError);
                _strToken = "Error";
            }

            //Verifica si es un identificador (Para mandarlo a la tabla de simbolos)
            if (_strToken == "IDEN")
            {
                MostrarTablaSimbolos(_strIden, ref _strToken);
            }

            //Verifica si es un delimitador
            if (_strToken == "C5")
            {
                _strToken = "DEL";
            }

            //Verificar si se realizara un salto de linea
            if (_blnMismaLinea)
            {
                _strTokens += _strToken + " ";
            }
            else
            {
                _intLineaError++;
                _strTokens += _strToken + Environment.NewLine;
            }

            //Muestra el token
            txtTokens.Text = _strTokens;   
        }
        
        public void MostrarTablaSimbolos(string _strIdentificador, ref string _strToken)
        {
            dgvTablaSimbolos.Rows.Clear();
            
            
            foreach (string otroIdentificador in misIdentificadores)
            {
                if (otroIdentificador == _strIdentificador)
                {
                    _blnIdenRepetido = true;
                }
            }

            if (!_blnIdenRepetido)
            {
                _intNumeroIdentificador++;
                misIdentificadores.Add(_strIdentificador);
                misNumerosIdentificadores.Add(_intNumeroIdentificador);
            }

            for (int i = 0; i < misIdentificadores.Count(); i++)
            {
                dgvTablaSimbolos.Rows.Add(misNumerosIdentificadores[i], misIdentificadores[i]);
                if (misIdentificadores[i] == _strIdentificador)
                {
                    _strToken += misNumerosIdentificadores[i];
                }
            }

            _blnIdenRepetido = false;
        }

        public void MostrarErrorLexico(int _intNumeroError, int _intLineaError)
        {
            string _strErrorLexico = "";
            _intCantidadErrores++;

            switch (_intNumeroError)
            {
                case 180:
                    _strErrorLexico = "Identificador no valido";
                    break;

                case 181:
                    _strErrorLexico = "Palabra reservada no valida";
                    break;

                case 182:
                    _strErrorLexico = "Constante entera no valida";
                    break;

                case 183:
                    _strErrorLexico = "Constante real no valida";
                    break;

                case 184:
                    _strErrorLexico = "Constante exponencial no valida";
                    break;

                case 185:
                    _strErrorLexico = "Operador aritmetico no valido";
                    break;

                case 186:
                    _strErrorLexico = "Operador relacional no valido";
                    break;

                case 187:
                    _strErrorLexico = "Operador logico no valido";
                    break;

                case 188:
                    _strErrorLexico = "Caracter especial no valido";
                    break;

                case 189:
                    _strErrorLexico = "Cadena no valida";
                    break;

                case 190:
                    _strErrorLexico = "Comentario no valido";
                    break;

                case 191:
                    _strErrorLexico = "Asignacion no valida";
                    break;
            }
            dgvErroresLexicos.Rows.Add(_intLineaError, _strErrorLexico);
        }

        private void txtPrograma_TextChanged(object sender, EventArgs e)
        {
            numeroLineas = txtPrograma.Lines.Length;

            string numerosLinea = "";

            for (int i = 1; i <= numeroLineas; i++)
            {
                numerosLinea += i + Environment.NewLine;
            }

            textBox1.Text = numerosLinea;
        }

        private void txtPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox1.Lines.Length == 17)
            {
                e.Handled = true; // Suprime la tecla Enter
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                e.Handled = true;
            }
        }

        private void btnGuardarPrograma_Click(object sender, EventArgs e)
        {
            Guardar(txtPrograma.Text);
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            txtPrograma.Enabled = true;
            btnHabilitar.Enabled = false;
        }

        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            Guardar(txtTokens.Text);
        }

        public void Guardar(string _strTexto)
        {
            // Abre un cuadro de diálogo para seleccionar la ubicación y el nombre del archivo
            SaveFileDialog miArchivoGuardado = new SaveFileDialog();
            miArchivoGuardado.Filter = "Archivos de texto (*.txt)|*.txt";
            miArchivoGuardado.FilterIndex = 1;

            if (miArchivoGuardado.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Guarda el contenido del TextBox en el archivo seleccionado
                    File.WriteAllText(miArchivoGuardado.FileName, _strTexto);
                    MessageBox.Show("El archivo se ha guardado correctamente.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar el archivo: " + ex.Message);
                }
            }
        }

        private void txtPrograma_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Evita que el Tab cambie el control enfocado
                e.IsInputKey = true;

                // Inserta cinco espacios en la posición actual del cursor
                int currentPosition = txtPrograma.SelectionStart;
                txtPrograma.Text = txtPrograma.Text.Insert(currentPosition, new string(' ', 5));

                // Mueve el cursor cinco espacios hacia la derecha
                txtPrograma.SelectionStart = currentPosition + 5;
            }
        }
    }
}