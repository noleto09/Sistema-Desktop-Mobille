using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Agrotech
{
    public partial class MainPage : ContentPage
    {
        private bool isPasswordVisible = false;
        private DataAccess dataAccess = new DataAccess();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            // Lógica para o link "Esqueceu a senha?"
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            Password.IsPassword = !isPasswordVisible;
            ((ImageButton)sender).Source = isPasswordVisible ? "eye_icon_open.png" : "eye_icon.png";
        }

        private async void Login_Clicked_1(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            // Obtenha o nome do usuário se as credenciais forem válidas
            string nomeUsuario = await dataAccess.VerificarUsuarioAsync(username, password);

            if (nomeUsuario != null)
            {
                // Armazene o nome do usuário logado
                UsuarioLogado.NomeUsuario = nomeUsuario;

                // Navegue para a página principal ou de vendas
                await Navigation.PushAsync(new Paginas());
            }
            else
            {
                await DisplayAlert("Erro de Login", "Usuário não cadastrado, usuário ou senha incorreta", "OK");
            }
        }


        // Evento acionado ao pressionar "Enter" no campo Username
        private void OnUsernameCompleted(object sender, EventArgs e)
        {
            Password.Focus(); // Move o foco para o campo de senha
        }

        // Evento acionado ao pressionar "Enter" no campo Password
        private void OnPasswordCompleted(object sender, EventArgs e)
        {
            Login_Clicked_1(sender, e); // Chama o método de login
        }
    }
}
