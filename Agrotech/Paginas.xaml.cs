namespace Agrotech;

public partial class Paginas : ContentPage
{
	public Paginas()
	{
		InitializeComponent();
	}

    private async void Button_Clicked_Insumo(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroInsumo());
    }

    private async void Button_Clicked_Produto(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroProduto());
    }

    private async void Button_Clicked_Retirar_insumo(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RetirarInsumo());
    }
}