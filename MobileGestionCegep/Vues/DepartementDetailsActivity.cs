using Android.Content;
using Android.Views;
using AndroidCegep2024.Controleurs;
using AndroidCegep2024.DTOs;
using AndroidCegep2024.Modeles;
using AndroidCegep2024.Utils;


namespace MobileGestionCegep.Vues
{
    /// <summary>
    /// Activité pour afficher les détails d'un département.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class DepartementDetailsActivity : Activity
    {
        /// <summary>
        /// Attribut représentant le paramètre du nom du Cégep reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre reçu du nom du département reçu de l'activité précédente.
        /// </summary>
        private string paramNomDepartement;

        /// <summary>
        /// Attribut représentant le département.
        /// </summary>
        private DepartementDTO leDepartement;

        /// <summary>
        /// Attribut représentant l'étiquette pour le nom du département.
        /// </summary>
        private TextView lblNomDepartement;

        /// <summary>
        /// Attribut représentant l'étiquette pour le no du département.
        /// </summary>
        private TextView lblNoDepartement;

        /// <summary>
        /// Attribut représentant l'étiquette pour la description du département.
        /// </summary>
        private TextView lblDescriptionDepartement;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DepartementDetails_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            Title = paramNomCegep;

            lblNomDepartement = FindViewById<TextView>(Resource.Id.lblNomDepartementAfficher);
            lblNoDepartement = FindViewById<TextView>(Resource.Id.lblNoDepartementAfficher);
            lblDescriptionDepartement = FindViewById<TextView>(Resource.Id.lblDescriptionDepartementAfficher);
        }

        /// <summary>
        /// Méthode de service appelée lors du retour en avant plan de l'activité.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            RafraichirInterfaceDonnees();
        }

        /// <summary>
        /// Méthode permettant de rafraichir les informations du Cégep...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                leDepartement = CegepControleur.Instance.ObtenirDepartement(paramNomCegep, paramNomDepartement);
                lblNomDepartement.Text = leDepartement.Nom;
                lblNoDepartement.Text = leDepartement.No;
                lblDescriptionDepartement.Text = leDepartement.Description;
            }
            catch (Exception)
            {
                Finish();
            }
        }
        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité principale.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Correction: Use the correct resource name as defined in ResourceConstant.Menu
            MenuInflater.Inflate(Resource.Menu.DepartementDetail_ActivityMenue, menu);
            return base.OnCreateOptionsMenu(menu);
        }

       }
}

