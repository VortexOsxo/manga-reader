using MangaReader.Services.Authentication;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MangaReader.Pages
{
    public partial class AuthenticationPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string FormTitle { get { return state.StateTitle; } }
        public string NextStateTitle { get { return state.NextStateTitle; } }
        public string ErrorMessage { get; set; }


        private AuthenticationState state;

        public AuthenticationPage()
        {
            state = new LoginState();
            state.AuthenticationPage = this;

            ErrorMessage = string.Empty;

            InitializeComponent();
            DataContext = this;
        }

        private void UpdateState_Click(object sender, RoutedEventArgs e)
        {
            state = state.GetNextState();
            state.AuthenticationPage = this;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NextStateTitle)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FormTitle)));
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            state.OnSubmit(username, password);
        }

        private void LoginCallback(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                NavigationService.GetNavigationService(this).Navigate(new SelectionPage());
                ErrorMessage = "";
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                ErrorMessage = "No account has this username";
            } else
            {
                ErrorMessage = "The password was invalid";
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
        }

        private void SignupCallback(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Created)
            {
                state = state.GetNextState();
                state.AuthenticationPage = this;
                ErrorMessage = "Account created, you can login";
            } else
            {
                ErrorMessage = "Username already used";
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
        }


        private interface AuthenticationState
        {
            public AuthenticationPage AuthenticationPage { get; set; }
            public string StateTitle { get; }
            public string NextStateTitle { get; }

            public void OnSubmit(string username, string password);

            public AuthenticationState GetNextState();
        }

        private class LoginState : AuthenticationState
        {
            public AuthenticationPage AuthenticationPage { get; set; }
            public string StateTitle { get { return "Log In"; } }
            public string NextStateTitle { get { return "Sign up"; } }

            public void OnSubmit(string username, string password) {
                LoginService.Login(username, password).ContinueWith(
                    (antecedant) => AuthenticationPage.Dispatcher.Invoke(() => AuthenticationPage.LoginCallback(antecedant.Result))
                );
            }

            public AuthenticationState GetNextState() { return new SignupState(); }
        }

        private class SignupState : AuthenticationState
        {
            public AuthenticationPage AuthenticationPage { get; set; }
            public string StateTitle { get { return "Sign Up"; } }
            public string NextStateTitle { get { return "Log in"; } }

            public void OnSubmit(string username, string password) { 
                SignupService.Signup(username, password).ContinueWith(
                    (antecedant) => AuthenticationPage.Dispatcher.Invoke(() => AuthenticationPage.SignupCallback(antecedant.Result))
                );
            }

            public AuthenticationState GetNextState() { return new LoginState(); }
        }
    }
}
