using Android.Views;
using AndroidCegep2024.DTOs;
using MobileGestionCegep;

/// <summary>
/// Namespace pour les adapteurs.
/// </summary>
namespace GestionCegepMobile.Adapters
{
    /// <summary>
    /// Classe représentant un adapteur pour une liste de cours(s).
    /// </summary>
    public class ListeCoursAdapter : BaseAdapter<CoursDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
		private Activity context;
        /// <summary>
        /// Attribut représentant la liste de Cours.
        /// </summary>
		private CoursDTO[] listeCours;

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="unContext">Contexte.</param>
        /// <param name="uneListeCegep">Liste des cours.</param>
		public ListeCoursAdapter(Activity unContext, CoursDTO[] uneListeCours)
        {
            context = unContext;
            listeCours = uneListeCours;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste de cours selon un index.
        /// </summary>
        /// <param name="index">Index du cours.</param>
        /// <returns>Retourne un CoursDTO contenant les informations du cours selon l'index passé en paramètre.</returns>
		public override CoursDTO this[int index]
        {
            get { return listeCours[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le Id d'un cours selon une position.
        /// </summary>
        /// <param name="position">Position du cours.</param>
        /// <returns>Retourne le ID du cours à la position demandée.</returns>
		public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de Cour(s) dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre de cour(s) dans la liste.</returns>
		public override int Count
        {
            get { return listeCours.Length; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le visuel d'un cours.
        /// </summary>
        /// <param name="position">Position du cours.</param>
        /// <param name="convertView">Vue.</param>
        /// <param name="parent">Parent de la vue.</param>
        /// <returns>Retourne une vue construite avec les données d'un cours.</returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeCoursItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomCours).Text = listeCours[position].Nom;

            return view;
        }
    }
}