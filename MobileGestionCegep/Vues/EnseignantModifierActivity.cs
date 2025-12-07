using Android.Content;
using Android.Views;
using AndroidCegep2024.Utils;
using AndroidCegep2024.Controleurs;
using AndroidCegep2024.DTOs;
using MobileGestionCegep;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace GestionCegepMobile.Vues
{
    /// <summary>
    /// Classe de type Activité pour la modification d'un Cégep.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class EnseignantModifierActivity : Activity
    {
        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomDepartement;

        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private int paramNoEnseignant;

        /// <summary>
        /// Attribut représentant le DTO de l'enseignant.
        /// </summary>
        private EnseignantDTO enseignant;

        /// <summary>
        /// Attribut représentant le champ d'édition du no de l'enseigant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtNoEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom de l'enseigant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtNomEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du prénom de l'enseigant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtPrenomEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition de l'adresse de l'enseignant la modification d'un enseignant. 
        /// </summary>
        private EditText edtAdresseEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition de la ville de l'enseignant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtVilleEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition de la province de l'enseignant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtProvinceEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du code postal de l'enseignant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtCodePostalEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du téléphone de l'enseignant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtTelephoneEnseignantModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du courriel de l'enseignant pour la modification d'un enseignant. 
        /// </summary>
        private EditText edtCourrielEnseignantModifier;

        /// <summary>
        /// Attribut représentant le bouton de modification.
        /// </summary>
        private Button btnModifierEnseignant;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EnseignantModifier_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNoEnseignant = Intent.GetIntExtra("paramNoEnseignant", 0);

            edtNoEnseignantModifier = FindViewById<EditText>(Resource.Id.edtNoModifier);
            edtNomEnseignantModifier = FindViewById<EditText>(Resource.Id.edtNomModifier);
            edtPrenomEnseignantModifier = FindViewById<EditText>(Resource.Id.edtPrenomEnseignantModifier);
            edtAdresseEnseignantModifier = FindViewById<EditText>(Resource.Id.edtAdresseModifier);
            edtVilleEnseignantModifier = FindViewById<EditText>(Resource.Id.edtVilleModifier);
            edtProvinceEnseignantModifier = FindViewById<EditText>(Resource.Id.edtProvinceModifier);
            edtCodePostalEnseignantModifier = FindViewById<EditText>(Resource.Id.edtCodePostalModifier);
            edtTelephoneEnseignantModifier = FindViewById<EditText>(Resource.Id.edtTelephoneModifier);
            edtCourrielEnseignantModifier = FindViewById<EditText>(Resource.Id.edtCourrielModifier);

            btnModifierEnseignant = FindViewById<Button>(Resource.Id.btnModifier);
            btnModifierEnseignant.Click += delegate
            {
                if((edtNomEnseignantModifier.Text.Length > 0) && (edtPrenomEnseignantModifier.Text.Length >0 ) && (edtAdresseEnseignantModifier.Text.Length > 0) && (edtVilleEnseignantModifier.Text.Length > 0) && (edtProvinceEnseignantModifier.Text.Length > 0) && (edtCodePostalEnseignantModifier.Text.Length > 0) && (edtTelephoneEnseignantModifier.Text.Length > 0) && (edtCourrielEnseignantModifier.Text.Length > 0))
                {
                    try
                    {
                        CegepControleur.Instance.ModifierEnseignant(paramNomCegep, paramNomDepartement, new EnseignantDTO(int.Parse(edtNoEnseignantModifier.Text), edtNomEnseignantModifier.Text, edtPrenomEnseignantModifier.Text, edtAdresseEnseignantModifier.Text, edtVilleEnseignantModifier.Text, edtProvinceEnseignantModifier.Text, edtCodePostalEnseignantModifier.Text, edtTelephoneEnseignantModifier.Text, edtCourrielEnseignantModifier.Text));
                        DialoguesUtils.AfficherToasts(this, paramNoEnseignant + " : " +" modifiee");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "erreur", ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Champs non valide");
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
        /// Méthode permettant de rafraichir les informations du Cégep...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                enseignant = CegepControleur.Instance.ObtenirEnseignant(paramNomCegep, paramNomDepartement, enseignant);
                edtNoEnseignantModifier.Text = enseignant.NoEmploye.ToString();
                edtNomEnseignantModifier.Text = enseignant.Nom;
                edtPrenomEnseignantModifier.Text = enseignant.Prenom;
                edtAdresseEnseignantModifier.Text = enseignant.Adresse;
                edtVilleEnseignantModifier.Text = enseignant.Ville;
                edtProvinceEnseignantModifier.Text = enseignant.Province;
                edtCodePostalEnseignantModifier.Text = enseignant.CodePostal;
                edtTelephoneEnseignantModifier.Text = enseignant.Telephone;
                edtCourrielEnseignantModifier.Text = enseignant.Courriel;
                Title = enseignant.Nom + " , " + enseignant.Prenom;
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
            MenuInflater.Inflate(Resource.Menu.EnseignantModifier_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
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