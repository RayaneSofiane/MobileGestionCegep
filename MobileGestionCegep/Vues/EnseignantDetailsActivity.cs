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
    public class EnseignantDetailsActivity : Activity
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
        int paramNoEnseignant;

        private EnseignantDTO enseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du no de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblNoEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du nom de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblNomEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du prénom de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblPrenomEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette de l'adresse de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblAdresseEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette de la ville de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblVilleEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette de la province de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblProvinceEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du code postal de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblCodePostalEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du téléphone de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblTelephoneEnseignant;

        /// <summary>
        /// Attribut représentant l'étiquette du courriel de l'enseignant pour l'affichage d'un enseignant.
        /// </summary>
        private TextView lblCourrielEnseignant;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EnseignantDetail_Actibity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
            paramNoEnseignant = Intent.GetIntExtra("paramNoEnseignant", 0);

            lblNoEnseignant = FindViewById<TextView>(Resource.Id.lblNoEnseignantAfficher);
            lblNomEnseignant = FindViewById<TextView>(Resource.Id.lblNomEnseignantAfficher);
            lblPrenomEnseignant = FindViewById<TextView>(Resource.Id.lblPrenomEnseignantAfficher);
            lblAdresseEnseignant = FindViewById<TextView>(Resource.Id.lblAdresseEnseignantAfficher);
            lblVilleEnseignant = FindViewById<TextView>(Resource.Id.lblVilleEnseignantAfficher);
            lblProvinceEnseignant = FindViewById<TextView>(Resource.Id.lblProvinceEnseignantAfficher);
            lblCodePostalEnseignant = FindViewById<TextView>(Resource.Id.lblCodePostalEnseignantAfficher);
            lblTelephoneEnseignant = FindViewById<TextView>(Resource.Id.lblTelephoneEnseignantAfficher);
            lblCourrielEnseignant = FindViewById<TextView>(Resource.Id.lblCourrielEnseignantAfficher);
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
                enseignant = CegepControleur.Instance.ObtenirEnseignant(paramNomCegep, paramNomDepartement, enseignant);
                lblNoEnseignant.Text = enseignant.NoEmploye.ToString();
                lblNomEnseignant.Text = enseignant.Nom;
                lblPrenomEnseignant.Text = enseignant.Prenom;
                lblAdresseEnseignant.Text = enseignant.Adresse;
                lblVilleEnseignant.Text = enseignant.Ville;
                lblProvinceEnseignant.Text = enseignant.Province;
                lblCodePostalEnseignant.Text = enseignant.CodePostal;
                lblTelephoneEnseignant.Text = enseignant.Telephone;
                lblCourrielEnseignant.Text = enseignant.Courriel;
                Title = enseignant.Nom + " , " + enseignant.Prenom;
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
            MenuInflater.Inflate(Resource.Menu.EnseignantDetails_ActivityMenu, menu);
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

                    Intent activiteEnseignantModifier = new Intent(this, typeof(EnseignantModifierActivity));
                    //On initialise les paramètres avant de lancer la nouvelle activité.
                    activiteEnseignantModifier.PutExtra("paramNomCegep", paramNomCegep);
                    activiteEnseignantModifier.PutExtra("paramNomDepartement", paramNomDepartement);
                    activiteEnseignantModifier.PutExtra("paramNoEnseignant", paramNoEnseignant);
                    //On démarre la nouvelle activité.
                    StartActivity(activiteEnseignantModifier);
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
                                CegepControleur.Instance.SupprimerEnseignant(paramNomCegep, paramNomDepartement, enseignant);
                                Finish();
                            }
                            catch (Exception ex)
                            {
                                DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), ex.Message);
                            }
                        });
                        AlertDialog dialog = builder.Create();
                        dialog.SetTitle("Suppression");
                        dialog.SetMessage("Voulez-vous vraiment supprimer l'ensegniant ?");
                        dialog.Window.SetGravity(GravityFlags.Bottom);
                        dialog.Show();
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreure", ex.Message);
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