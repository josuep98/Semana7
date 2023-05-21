using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        private SQLiteAsyncConnection conexion;

        public Login()
        {
            InitializeComponent();
            conexion = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> Select_Where(SQLiteConnection db, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("Select * From Estudiante Where Usuario = ? and Contrasena = ?", usuario, contrasena);
        }

        private void BtnInicio_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                db.CreateTable<Estudiante>();

                IEnumerable<Estudiante> resultado = Select_Where(db, TxtUsuario.Text, TxtContrasena.Text);
                if (resultado.Any())
                    Navigation.PushAsync(new ConsultaRegistros());
                else
                    DisplayAlert("Alerte", "Usuario/Contraseña incorrectos", "Aceptar");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}