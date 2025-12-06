using Android.Views;
using AndroidCegep2024.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Namespace pour les adapteurs.
/// </summary>
namespace MobileGestionCegep.Adapteurs
{
    /// <summary>
    /// Classe représentant un adapteur pour une liste de Eneignants (s).
    /// </summary>
    public class ListeEnseignantAdapter : BaseAdapter<EnseignantDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
        private Activity context;
        /// <summary>
        /// Attribut représentant la liste de Enseignants.
        /// </summary>
        private EnseignantDTO[] listeEnseignant;

        /// <summary>
        /// Constructeur de la classe. 
        /// </summary>
        /// <param name="context">Contexte.</param>
        /// <param name="acteurs">Liste des Enseignants.</param>
        public ListeEnseignantAdapter(Activity unContext, EnseignantDTO[] uneListeEnseignant)
        {
            context = unContext;
            listeEnseignant = uneListeEnseignant;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste de Enseignants selon un index.
        /// </summary>
        /// <param name="index">Index de la garderie.</param>
        /// <returns>Retourne un EnseignantDTO contenant les informations de l'Enseignant selon l'index passé en paramètre.</returns>
        public override EnseignantDTO this[int index]
        {
            get { return listeEnseignant[index]; }
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir l'Id d'un Enseignant selon une position.
        /// </summary>
        /// <param name="position">Position de l'Enseignant.</param>
        /// <returns>Retourne le ID de l'Enseignant à la position demandée.</returns>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de Enseignant(s) dans la liste.
        /// </summary>
        /// <returns>Retourne le nombre de Enseignant(s) dans la liste.</returns>
        public override int Count
        {
            get { return listeEnseignant.Length; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> position de l'enseignant</param>
        /// <param name="convertView">vue</param>
        /// <param name="parent"> parent de la vue</param>
        /// <returns>Retourne une vue construite avec les données d'un enseignant.</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeEnseignantItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomEnseignant).Text = listeEnseignant[position].Nom;


            return view;
        }
    }
}
