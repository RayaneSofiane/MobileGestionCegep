using Android.Views;
using AndroidCegep2024.DTOs;

namespace MobileGestionCegep.Adapteurs
{
    /// <summary>
    /// classe representnt un adapteur pour une liste de département(s).
    /// </summary>
    public class ListeDepartementAdapter : BaseAdapter<DepartementDTO>
    {
        /// <summary>
        /// Attribut représetant le contexte.
        /// </summary>
        private Activity context;

        /// <summary>
        /// Attribut représentant la liste de Cegeps.
        /// </summary>
        private DepartementDTO[] listeDepartement;

        /// <summary>
        /// constructeur de la classe.
        /// </summary>
        /// <param name="unContext">context</param>
        /// <param name="uneListeDepartement"> liste de departement</param>
        public ListeDepartementAdapter(Activity unContext, DepartementDTO[] uneListeDepartement)
        {
            context = unContext;
            listeDepartement = uneListeDepartement;
        }

        /// <summary>
        /// Méthode réécrite de la classe BaseAdapter permettant d'accéder à un élément de la liste des departement selon un index.
        /// </summary>
        /// <param name="index">Index de la garderie.</param>
        /// <returns>Retourne un DepartementDTO contenant les informations du departement selon so index .</returns>
        public override DepartementDTO this[int index]
        {
            get { return listeDepartement[index]; }
        }
        /// <summary>
        /// methode reecrite de la classe BaseAdapter permettant d'obtenir le Id d'un departement selon une position.
        /// </summary>
        /// <param name="position"> position du departement</param>
        /// <returns></returns>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        ///  Méthode réécrite de la classe BaseAdapter permettant d'obtenir le nombre de departement dans la liste.
        /// </summary>
        /// /// <returns>Retourne le nombre de departement (s) dans la liste.</returns>
        public override int Count
        {
            get { return listeDepartement.Length; }
        }
        /// <summary>
        /// methode reecrite de la classe BaseAdapter permettant de retourner la vue d'un departement.
        /// </summary>
        /// <param name="position">position du departement</param>
        /// <param name="convertView">vue</param>
        /// <param name="parent"></param>
        /// <returns>Retourne une vue construite avec les données d'un Cégep</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view =
                (convertView ??
                   context.LayoutInflater.Inflate(
                        Resource.Layout.ListeDepartementsItem, parent, false)) as LinearLayout;

            view.FindViewById<TextView>(Resource.Id.tvNomDepartement).Text = listeDepartement[position].Nom;
            view.FindViewById<TextView>(Resource.Id.tvNoDepartement).Text = listeDepartement[position].No;
            view.FindViewById<TextView>(Resource.Id.tvDescriptionDepartement).Text = listeDepartement[position].Description;

            return view;
        }

    }
}
