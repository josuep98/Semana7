using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace Semana7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        public int Id;
        private SQLiteAsyncConnection conexion;
        IEnumerable<Estudiante> GlobalActualizar;
        IEnumerable<Estudiante> GlobalEliminar;

        public Elemento(int id, string nombre, string usuario, string contrasena)//Se puede recibir directamente el objeto estudiante
        {
            InitializeComponent();
            TxtId.Text = id.ToString();
            TxtNombre.Text = nombre;
            TxtUsuario.Text = usuario;
            TxtContrasena.Text = contrasena;
            conexion = DependencyService.Get<DataBase>().GetConnection();
            Id = id;
        }

        public static IEnumerable<Estudiante> Eliminar(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("Delete from Estudiante where Id  = ?", id);
        }

        public static IEnumerable<Estudiante> Actualizar(SQLiteConnection db, string nombre, string usuario, string contrasena, int id)
        {
            return db.Query<Estudiante>("Update Estudiante set Nombre = ?, Usuario = ?, Contrasena = ? where Id = ?", nombre, usuario, contrasena, id);
        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                GlobalActualizar = Actualizar(db, TxtNombre.Text, TxtUsuario.Text, TxtContrasena.Text, Id);
                Navigation.PushAsync(new ConsultaRegistros());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                GlobalEliminar = Eliminar(db, Id);
                Navigation.PushAsync(new ConsultaRegistros());
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
    }
}