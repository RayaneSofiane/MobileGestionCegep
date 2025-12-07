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
    public class CoursModifierActivity : Activity
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
        private string paramNomCours;

        /// <summary>
        /// Attribut représentant le DTO du cours.
        /// </summary>
        private CoursDTO cours;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom du cours pour la modification d'un cours. 
        /// </summary>
        private EditText edtNomCoursModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du no du cours pour la modification d'un cours. 
        /// </summary>
        private EditText edtNoCoursModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition de la description d'un cours pour la modification d'un cours. 
        /// </summary>
        private EditText edtDescriptionCoursModifier;

        /// <summary>
        /// Attribut représentant le bouton de modification.
        /// </summary>
        private Button btnModifierCours;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CoursModifier_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNomCours = Intent.GetStringExtra("paramNomCours");

            edtNomCoursModifier = FindViewById<EditText>(Resource.Id.edtNomModifier);
            edtNoCoursModifier = FindViewById<EditText>(Resource.Id.edtNoModifier);
            edtDescriptionCoursModifier = FindViewById<EditText>(Resource.Id.edtDescriptionModifier);

            btnModifierCours = FindViewById<Button>(Resource.Id.btnModifier);
            btnModifierCours.Click += delegate
            {
                if ((edtNoCoursModifier.Text.Length > 0) && (edtDescriptionCoursModifier.Text.Length > 0))
                {
                    try
                    {
                        CegepControleur.Instance.ModifierCours(paramNomCegep, paramNomDepartement, new CoursDTO(edtNoCoursModifier.Text, edtNomCoursModifier.Text, edtDescriptionCoursModifier.Text));
                        DialoguesUtils.AfficherToasts(this, paramNomCours + " modifié");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Veuillez remplir tous les champs.");
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
                cours = CegepControleur.Instance.ObtenirCours(paramNomCegep, paramNomDepartement, cours);
                edtNomCoursModifier.Text = cours.Nom;
                edtNoCoursModifier.Text = cours.No;
                edtDescriptionCoursModifier.Text = cours.Description;
                Title = cours.Nom;
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
            MenuInflater.Inflate(Resource.Menu.CoursModifier_ActivityMenu, menu);
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