using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EmotionPlatzi.Web.Models
{
    //Problemas de migración al agregar notificaciones
    //Tutorial para crear base de datos en azutre
    //Herramientas ->Administrador de paquetes Nuguet-> Consola del Administrador de Paquetes
    //Seleccionar proyecto ---- escribir en consola Enable-Migrations-EnableAutomaticMigrations
    //Or Buscar Enable-Migration,Add-Migration, Update-Database
    //¿Cómo hacer migraciones? http:EntityFrameworkTutorial.net
    //Validaciones dinámicas https://jqueryvalidation.org
    public class EmoPicture
    {
        public int Id { get; set; }
        [Display(Name="Nombre")]
        //[Display(Name = "labelForName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Required]
        //[MaxLength(10,ErrorMessage ="La ruta supera el tamaño establecido")]
        //[RegularExpression()]
        public string Path { get; set; }

        //Una pintura tiene muchos rostros
        //Propiedades de Navegación (Estan en el modelo pero no equivalen a un campo real del modelo, 
        //la utiliza Entiti para determinar la relación/dependecia maestro-detalle
        public virtual ObservableCollection<EmoFace> Faces  { get; set; }

    }
}