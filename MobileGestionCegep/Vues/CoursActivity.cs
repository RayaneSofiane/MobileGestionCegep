using Android.Content;
using Android.Views;
using AndroidCegep2024.Utils;
using AndroidCegep2024.Controleurs;
using AndroidCegep2024.DTOs;
using MobileGestionCegep;
using GestionCegepMobile.Adapters;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour l'affichage des cours et l'ajout d'un cours dans un département.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class CoursActivity : Activity
    {
        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        string paramNomDepartement;

        /// <summary>
        /// Liste de cours.
        /// </summary>
        private CoursDTO[] listeCours;

        /// <summary>
        /// Adapteur de la liste de cours.
        /// </summary>
        private ListeCoursAdapter adapteurListeCours;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom du cours pour l'ajout d'un cours.
        /// </summary>
        private EditText edtNomCours;

        /// <summary>
        /// Attribut représentant le champ d'édition du no du cours pour l'ajout d'un cours.
        /// </summary>
        private EditText edtNoCours;

        /// <summary>
        /// Attribut représentant le champ d'édition de la description du cours pour l'ajout d'un cours.
        /// </summary>
        private EditText edtDescriptionCours;

        /// <summary>
        /// Attribut représentant le bouton pour l'ajout d'un cours.
        /// </summary>
        private Button btnAjouterCours;

        /// <summary>
        /// Attribut représentant le listView pour la liste des cours.
        /// </summary>
        private ListView listViewCours;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Cours_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            Title = paramNomDepartement;

            edtNomCours = FindViewById<EditText>(Resource.Id.edtNomInfo);
            edtNoCours = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtDescriptionCours = FindViewById<EditText>(Resource.Id.edtDescriptionInfo);

            btnAjouterCours = FindViewById<Button>(Resource.Id.btnAjouter);
            btnAjouterCours.Click += delegate
            {
                if ((edtNomCours.Text.Length > 0) && (edtNomCours.Text.Length > 0) && (edtDescriptionCours.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomCours.Text;
                        CegepControleur.Instance.AjouterCours(paramNomCegep, paramNomDepartement, new CoursDTO(edtNoCours.Text, edtNomCours.Text, edtDescriptionCours.Text));
                        RafraichirInterfaceDonnees();
                        DialoguesUtils.AfficherToasts(this, nom + " ajouté");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), "Veuillez remplir tous les champs.");
            };

            listViewCours = FindViewById<ListView>(Resource.Id.listViewCours);
            listViewCours.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteCoursDetails = new Intent(this, typeof(CoursDetailsActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteCoursDetails.PutExtra("paramNomCegep", paramNomCegep);
                activiteCoursDetails.PutExtra("paramNomDepartement", paramNomDepartement);
                activiteCoursDetails.PutExtra("paramNomCours", listeCours[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteCoursDetails);
            };
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
        /// Méthode permettant de rafraichir la liste des Cégeps...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                listeCours = CegepControleur.Instance.ObtenirListeCours(paramNomCegep, paramNomDepartement).ToArray();
                adapteurListeCours = new ListeCoursAdapter(this, listeCours);
                listViewCours.Adapter = adapteurListeCours;
                edtNomCours.Text = edtNoCours.Text = edtDescriptionCours.Text = "";
                edtNomCours.RequestFocus();
            }
            catch (Exception)
            {
                Finish();
            }
        }
        /// <summary>Méthode de service permettant d'initialiser le menu de l'activité.</summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Cours_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.ViderListe:
                    try
                    {
                        CegepControleur.Instance.ViderListeCours(paramNomCegep, paramNomDepartement);
                        DialoguesUtils.AfficherToasts(this, "Liste vidée avec succès!");
                        RafraichirInterfaceDonnees();
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                    break;

                case Resource.Id.Retour:
                    Finish();
                    break;

                case Resource.Id.Quitter:
                    FinishAffinity();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}