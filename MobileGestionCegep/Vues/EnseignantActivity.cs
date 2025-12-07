using Android.Content;
using Android.Views;
using AndroidCegep2024.Utils;
using AndroidCegep2024.Controleurs;
using AndroidCegep2024.DTOs;
using MobileGestionCegep;
using MobileGestionCegep.Adapteurs;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour l'affichage des enseignant et l'ajout d'un enseignant dans un département.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class EnseignantActivity : Activity
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
        /// Liste de enseignants.
        /// </summary>
        private EnseignantDTO[] listeEnseignant;

        /// <summary>
        /// Adapteur de la liste de enseignants.
        /// </summary>
        private ListeEnseignantAdapter adapteurListeEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du no de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtNoEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtNomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du prénom de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtPrenomEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de l'adresse de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtAdresseEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la ville de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtVilleEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition de la province de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtProvinceEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du téléphone de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du courriel de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtCourrielEnseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du code postal de l'enseignant pour l'ajout d'un enseignant.
        /// </summary>
        private EditText edtCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant le bouton pour l'ajout d'un enseignant.
        /// </summary>
        private Button btnAjouterCegep;

        /// <summary>
        /// Attribut représentant le listView pour la liste des enseignants.
        /// </summary>
        private ListView listViewEnseignant;
        

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Enseignant_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            Title = paramNomDepartement;

            edtNoEnseignant = FindViewById<EditText>(Resource.Id.edtNoInfo);
            edtNomEnseignant = FindViewById<EditText>(Resource.Id.edtNomEnseignant);
            edtPrenomEnseignant = FindViewById<EditText>(Resource.Id.edtPrenomEnseignantin);
            edtAdresseEnseignant = FindViewById<EditText>(Resource.Id.edtAdresseInfo);
            edtVilleEnseignant = FindViewById<EditText>(Resource.Id.edtVilleInfo);
            edtProvinceEnseignant = FindViewById<EditText>(Resource.Id.edtProvinceInfo);
            edtCodePostalEnseignant = FindViewById<EditText>(Resource.Id.edtCodePostalInfo);
            edtTelephoneEnseignant = FindViewById<EditText>(Resource.Id.edtTelephoneInfo);
            edtCourrielEnseignant = FindViewById<EditText>(Resource.Id.edtCourrielInfo);

            btnAjouterCegep = FindViewById<Button>(Resource.Id.btnAjouter);
            btnAjouterCegep.Click += delegate
            {
                if ((edtNoEnseignant.Text.Length > 0) && (int.TryParse(edtNoEnseignant.Text, out int noEnseignant)) && (edtNomEnseignant.Text.Length > 0) && (edtPrenomEnseignant.Text.Length > 0) && (edtAdresseEnseignant.Text.Length > 0) && (edtVilleEnseignant.Text.Length > 0) && (edtProvinceEnseignant.Text.Length > 0) && (edtCodePostalEnseignant.Text.Length > 0) && (edtTelephoneEnseignant.Text.Length > 0) && (edtCourrielEnseignant.Text.Length > 0))
                {
                    try
                    {
                        string nomComplet = edtPrenomEnseignant.Text + edtNomEnseignant.Text;
                        CegepControleur.Instance.AjouterEnseignant(paramNomCegep, paramNomDepartement, new EnseignantDTO(noEnseignant, edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text));
                        RafraichirInterfaceDonnees();
                        DialoguesUtils.AfficherToasts(this, nomComplet + " ajouté avec succès.");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Veuillez remplir tous les champs correctement.");
            };

            listViewEnseignant = FindViewById<ListView>(Resource.Id.listViewEnseignant);
            listViewEnseignant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteEnseignantDetails = new Intent(this, typeof(EnseignantDetailsActivity));
                activiteEnseignantDetails.PutExtra("paramNomCegep", paramNomCegep);
                activiteEnseignantDetails.PutExtra("paramNomDepartement", paramNomDepartement);
                activiteEnseignantDetails.PutExtra("paramNoEnseignant", listeEnseignant[e.Position].NoEmploye);
            
                StartActivity(activiteEnseignantDetails);
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
                listeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(paramNomCegep, paramNomDepartement).ToArray();
                adapteurListeEnseignant = new ListeEnseignantAdapter(this, listeEnseignant);
                listViewEnseignant.Adapter = adapteurListeEnseignant;
                edtNoEnseignant.Text = edtNomEnseignant.Text = edtPrenomEnseignant.Text = edtAdresseEnseignant.Text = edtVilleEnseignant.Text = edtProvinceEnseignant.Text = edtCodePostalEnseignant.Text = edtTelephoneEnseignant.Text = edtCourrielEnseignant.Text = "";
                edtNoEnseignant.RequestFocus();
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
            MenuInflater.Inflate(Resource.Menu.Enseignant_ActiviyMenu, menu);
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
                        CegepControleur.Instance.ViderListeEnseignant(paramNomCegep, paramNomDepartement);
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