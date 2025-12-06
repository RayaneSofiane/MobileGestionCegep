using Android.Views;

namespace AndroidCegep2024.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class DialoguesUtils
    {
        /// <summary>
        /// Méthoder permettant d'afficher un message éphémère...
        /// </summary>
        /// <param name="sender">L'Acitivité parente.</param>
        /// <param name="message">Le message.</param>
        public static void AfficherToasts(Activity sender, string message)
        {
            Toast.MakeText(sender, message, ToastLength.Long).Show();
        }

        /// <summary>
        /// Méthode permettant d'afficher un message OK.
        /// </summary>
        /// <param name="sender">L'Acitivité parente.</param>
        /// <param name="titre">Le titre.</param>
        /// <param name="message">Le message.</param>
        public static void AfficherMessageOK(Activity sender, string titre, string message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(sender);
            builder.SetPositiveButton("OK", (send, args) => { });
            AlertDialog dialog = builder.Create();
            dialog.SetTitle(titre);
            dialog.SetMessage(message);
            dialog.Show();
        }

        /// <summary>
        /// Méthode permettant d'afficher un message Oui Non.
        /// </summary>
        /// <param name="sender">L'activité parente.</param>
        /// <param name="titre">Le titre.</param>
        /// <param name="question">La question.</param>
        /// <param name="callback">La méthode callback.</param>
        public static void AfficherMessageOuiNon(Activity sender, string titre, string question, Action<bool> callback)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(sender);
            builder.SetNegativeButton("Oui", (send, args) =>
            {
                callback(true);
            });
            builder.SetPositiveButton("Non", (send, args) =>
            {
                callback(false);
            });
            AlertDialog dialog = builder.Create();
            dialog.SetTitle(titre);
            dialog.SetMessage(question);
            dialog.Window.SetGravity(GravityFlags.Bottom);
            dialog.Show();
        }
    }
}