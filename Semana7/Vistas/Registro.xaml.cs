using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Registro()
        {
            InitializeComponent();
            conexion = DependencyService.Get<DataBase>().GetConnection();
        }

        private void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var datos = new Estudiante
                {
                    Nombre = TxtNombre.Text,
                    Usuario = TxtUsuario.Text,
                    Contrasena = TxtContrasena.Text
                };

                conexion.InsertAsync(datos);

                TxtNombre.Text = string.Empty;
                TxtUsuario.Text = string.Empty;
                TxtContrasena.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error!", ex.Message, "Aceptar");
            }
        }
    }
}