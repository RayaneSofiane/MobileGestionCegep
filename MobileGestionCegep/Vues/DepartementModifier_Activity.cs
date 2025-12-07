using Android.Content;
using Android.Views;
using AndroidCegep2024.Utils;
using AndroidCegep2024.Controleurs;
using AndroidCegep2024.DTOs;
using MobileGestionCegep;

namespace AndroidCegep2024.Vues
{
    /// <summary>
    /// Classe de type Activité pour la modification d'un Département.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class DepartementModifier_Activity : Activity
    {
        /// <summary>
        /// Attribut représentant le paramètre du nom du Cégep reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le paramètre du nom du Département reçu de l'activité précédente.
        /// </summary>
        private string paramNomDepartement;

        /// <summary>
        /// Attribut représentant le DTO du Département.
        /// </summary>
        private DepartementDTO leDepartement;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom du Département.
        /// </summary>
        private EditText edtNomDepartementModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition du nom du Département.
        /// </summary>
        private EditText edtNoDepartementModifier;

        /// <summary>
        /// Attribut représentant le champ d'édition de la description du Département.
        /// </summary>
        private EditText edtDescriptionDepartementModifier;

        /// <summary>
        /// Attribut représentant le bouton de modification du Département.
        /// </summary>
        private Button btnModifierDepartement;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DepartementModifier_Activity);

            edtNomDepartementModifier = FindViewById<EditText>(Resource.Id.edtNomModifier);
            edtDescriptionDepartementModifier = FindViewById<EditText>(Resource.Id.edtDescriptionModifier);
            edtNoDepartementModifier = FindViewById<EditText>(Resource.Id.edtNoModifier);
            btnModifierDepartement = FindViewById<Button>(Resource.Id.btnModifier);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");

            btnModifierDepartement.Click += delegate
            {
                if (edtNoDepartementModifier.Text.Length > 0 && edtDescriptionDepartementModifier.Text.Length > 0)
                {
                    
                        CegepControleur.Instance.ModifierDepartement(
                            paramNomCegep,
                            new DepartementDTO(
                                no : edtNoDepartementModifier.Text,
                                nom: edtNomDepartementModifier.Text,
                                description: edtDescriptionDepartementModifier.Text
                            ));
                        DialoguesUtils.AfficherToasts(this, "Département modifié avec succès!");
                        Finish();
                  }                          
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
        /// Méthode permettant de rafraichir les informations du Département.
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                leDepartement = CegepControleur.Instance.ObtenirDepartement( paramNomCegep, paramNomDepartement);

                edtNoDepartementModifier.Text = leDepartement.No;
                edtNomDepartementModifier.Text = leDepartement.Nom;
                edtDescriptionDepartementModifier.Text = leDepartement.Description;
            }
            catch (Exception)
            {
                Finish();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'initialiser le menu de l'activité.
        /// </summary>
        /// <param name="menu">Le menu à construire.</param>
        /// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DepartementModifier_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// Méthode de service permettant de gérer les événements de sélection des options du menu.
        /// </summary>
        /// <param name="item">L'item sélectionné dans le menu.</param>
        /// <returns>Retourne True si l'événement est traité.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Retour:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
