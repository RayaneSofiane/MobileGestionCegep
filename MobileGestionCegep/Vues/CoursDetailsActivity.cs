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
    /// Classe de type Activité pour l'affichage des détails d'un enseignant.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class CoursDetailsActivity : Activity
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
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        string paramNomCours;

        private CoursDTO cours;

        /// <summary>
        /// Attribut représentant l'étiquette du nom du cours pour l'affichage d'un cours.
        /// </summary>
        private TextView lblNomCours;

        /// <summary>
        /// Attribut représentant l'étiquette du no du cours pour l'affichage d'un cours.
        /// </summary>
        private TextView lblNoCours;

        /// <summary>
        /// Attribut représentant l'étiquette de la description d'un cours pour l'affichage d'un cours.
        /// </summary>
        private TextView lblDescriptionCours;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CoursDetails_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNomCours = Intent.GetStringExtra("paramNomCours");

            lblNomCours = FindViewById<TextView>(Resource.Id.lblNomCoursAfficher);
            lblNoCours = FindViewById<TextView>(Resource.Id.lblNoCoursAfficher);
            lblDescriptionCours = FindViewById<TextView>(Resource.Id.lblDescriptionCoursAfficher);
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
        /// Méthode permettant de rafraichir la liste des cours...
        /// </summary>
        private void RafraichirInterfaceDonnees()
        {
            try
            {
                cours = CegepControleur.Instance.ObtenirCours(paramNomCegep, paramNomDepartement,cours);
                lblNomCours.Text = cours.Nom;
                lblNoCours.Text = cours.No;
                lblDescriptionCours.Text = cours.Description;
                Title = cours.Nom;
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
            MenuInflater.Inflate(Resource.Menu.CoursDetails_ActivityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>Méthode de service permettant de capter l'évenement exécuté lors d'un choix dans le menu.</summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Modifier:

                    Intent activiteCoursModifier = new Intent(this, typeof(CoursModifierActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteCoursModifier.PutExtra("paramNomCegep", paramNomCegep);
                    activiteCoursModifier.PutExtra("paramNomDepartement", paramNomDepartement);
                    activiteCoursModifier.PutExtra("paramNomCours", paramNomCours);
                    //On démarre la nouvelle activité.
                    StartActivity(activiteCoursModifier);
                    break;

                case Resource.Id.Supprimer:
                    try
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetPositiveButton(GetString(Android.Resource.String.No), (send, args) => { });
                        builder.SetNegativeButton(GetString(Android.Resource.String.Yes), (send, args) =>
                        {
                            try
                            {
                                CegepControleur.Instance.SupprimerCours(paramNomCegep, paramNomDepartement, cours);
                                Finish();
                            }
                            catch (Exception ex)
                            {
                                DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                            }
                        });
                        AlertDialog dialog = builder.Create();
                        dialog.SetTitle("Suppression");
                        dialog.SetMessage("Voulez-vous vraiment supprimer ce cours ?");
                        dialog.Window.SetGravity(GravityFlags.Bottom);
                        dialog.Show();
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