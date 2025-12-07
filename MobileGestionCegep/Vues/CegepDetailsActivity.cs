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
    /// Classe de type Activité pour la gestion d'un Cégep.
    /// </summary>
    [Activity(Label = "@string/app_name")]
    public class CegepDetailsActivity : Activity
    {
        /// <summary>
        /// Attribut représentant le paramètre reçu de l'activité précédente.
        /// </summary>
        private string paramNomCegep;

        /// <summary>
        /// Attribut représentant le DTO du Cégep.
        /// </summary>
        private CegepDTO leCegep;

        /// <summary>
        /// Attribut représentant la liste des départements.
        /// </summary>
        private List<DepartementDTO> listeDepartement;

        /// <summary>
        /// Attribut représentant l'adapteur pour le listView de la liste des départements.
        /// </summary>
        private ListeDepartementAdapter adapteurListeDepartement;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du nom du Cégep.
        /// </summary>
        private TextView lblNomCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de l'adresse du Cégep.
        /// </summary>
        private TextView lblAdresseCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la ville du Cégep.
        /// </summary>
        private TextView lblVilleCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage de la province du Cégep.
        /// </summary>
        private TextView lblProvinceCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du code postal du Cégep.
        /// </summary>
        private TextView lblCodePostalCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du téléphone du Cégep.
        /// </summary>
        private TextView lblTelephoneCegepAfficher;

        /// <summary>
        /// Attribut représentant l'étiquette pour l'affichage du courriel du Cégep.
        /// </summary>
        private TextView lblCourrielCegepAfficher;

        /// <summary>
        /// Attribut représentant le listView pour la liste des départements.
        /// </summary>
        private ListView listViewDepartement;

        /// <summary>
        /// Attribut représentant la boite d'édition du nom du département.
        /// </summary>
        private EditText edtNomDepartement;

        /// <summary>
        /// Attribut représentant la boite d'édition du no du département.
        /// </summary>
        private EditText edtNoDepartement;

        /// <summary>
        /// Attribut représentant la boite d'édition de la description du département.
        /// </summary>
        private EditText edtDescriptionDepartement;

        /// <summary>
        /// Attribut représentant le bouton pour l'ajout d'un département.
        /// </summary>
        private Button btnAjouter;

        /// <summary>
        /// Méthode de service appelée lors de la création de l'activité.
        /// </summary>
        /// <param name="savedInstanceState">État de l'activité.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CegepDetails_Activity);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            Title = paramNomCegep;

            lblNomCegepAfficher = FindViewById<TextView>(Resource.Id.lblNomAfficher);
            lblAdresseCegepAfficher = FindViewById<TextView>(Resource.Id.lblAdresseAfficher);
            lblVilleCegepAfficher = FindViewById<TextView>(Resource.Id.lblVilleAfficher);
            lblProvinceCegepAfficher = FindViewById<TextView>(Resource.Id.lblProvinceAfficher);
            lblCodePostalCegepAfficher = FindViewById<TextView>(Resource.Id.lblCodePostalAfficher);
            lblTelephoneCegepAfficher = FindViewById<TextView>(Resource.Id.lblTelephoneAfficher);
            lblCourrielCegepAfficher = FindViewById<TextView>(Resource.Id.lblCourrielAfficher);

            edtNomDepartement = FindViewById<EditText>(Resource.Id.edtNomDepartement);
            edtNoDepartement = FindViewById<EditText>(Resource.Id.edtNoDepartement);
            edtDescriptionDepartement = FindViewById<EditText>(Resource.Id.edtDescriptionDepartement);

            listViewDepartement = FindViewById<ListView>(Resource.Id.listViewDepartement);
            listViewDepartement.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteDepartementDetails = new Intent(this, typeof(DepartementDetailsActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteDepartementDetails.PutExtra("paramNomCegep", leCegep.Nom);
                activiteDepartementDetails.PutExtra("paramNomDepartement", listeDepartement[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteDepartementDetails);
            };

            btnAjouter = FindViewById<Button>(Resource.Id.btnAjouter);
            btnAjouter.Click += delegate
            {
                if ((edtNomDepartement.Text.Length > 0) && (edtNoDepartement.Text.Length > 0) && (edtDescriptionDepartement.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomDepartement.Text;
                        CegepControleur.Instance.AjouterDepartement(paramNomCegep, new DepartementDTO(edtNoDepartement.Text, edtNomDepartement.Text, edtDescriptionDepartement.Text));
                        RafraichirInterfaceDonnees();
                        DialoguesUtils.AfficherToasts(this, nom + " ajouté avec succès!");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), "Veuillez remplir tous les champs.");
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
                leCegep = CegepControleur.Instance.ObtenirCegep(paramNomCegep);
                lblNomCegepAfficher.Text = leCegep.Nom;
                lblAdresseCegepAfficher.Text = leCegep.Adresse;
                lblVilleCegepAfficher.Text = leCegep.Ville;
                lblProvinceCegepAfficher.Text = leCegep.Province;
                lblCodePostalCegepAfficher.Text = leCegep.CodePostal;
                lblTelephoneCegepAfficher.Text = leCegep.Telephone;
                lblCourrielCegepAfficher.Text = leCegep.Courriel;

                listeDepartement = CegepControleur.Instance.ObtenirListeDepartement(leCegep.Nom);
                adapteurListeDepartement = new ListeDepartementAdapter(this, listeDepartement.ToArray());
                listViewDepartement.Adapter = adapteurListeDepartement;
                edtNomDepartement.Text = edtNoDepartement.Text = edtDescriptionDepartement.Text = "";
                edtNomDepartement.RequestFocus();
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
            MenuInflater.Inflate(Resource.Menu.CegepDetails_ActivityMenu, menu);
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
                    Intent activiteModifier = new Intent(this, typeof(CegepModifierActivity));
                    activiteModifier.PutExtra("paramNomCegep", leCegep.Nom);
                    StartActivity(activiteModifier);
                    break;

                case Resource.Id.Supprimer:
                    try
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetPositiveButton("Non", (send, args) =>{});
                        builder.SetNegativeButton("Oui", (send, args) =>
                        {
                            try
                            {
                                CegepControleur.Instance.SupprimerCegep(leCegep.Nom);
                                Finish();
                            }
                            catch (Exception ex)
                            {
                                DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), ex.Message);
                            }
                        });
                        AlertDialog dialog = builder.Create();
                        dialog.SetTitle(GetString(Resource.String.app_name));
                        dialog.SetMessage("Voulez-vous vraiment supprimer ce cégep?");
                        dialog.Window.SetGravity(GravityFlags.Bottom);
                        dialog.Show();
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, GetString(Resource.String.app_name), ex.Message);
                    }
                    break;
                case Resource.Id.ViderListe:
                    try
                    {
                        CegepControleur.Instance.ViderListeDepartement(paramNomCegep);
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