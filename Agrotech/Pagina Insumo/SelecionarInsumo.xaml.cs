namespace Agrotech.Pagina_Insumo;

public partial class SelecionarInsumo : ContentPage
{
    private readonly DataAccess dataAccess = new DataAccess();

    public SelecionarInsumo()
    {
        InitializeComponent();
        LoadInsumosAsync();
    }

    private async Task LoadInsumosAsync()
    {
        try
        {
            var insumos = await dataAccess.GetAllInsumosAsync(); // Supondo que tenha um método para obter os insumos
            foreach (var insumo in insumos)
            {
                AddInsumoFrame(insumo);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar insumos: " + ex.Message);
        }
    }

    private void AddInsumoFrame(Insumo insumo)
    {
        Frame insumoFrame = new Frame
        {
            BackgroundColor = Color.FromRgb(255, 255, 255), // Branco usando FromRgb
            CornerRadius = 15,
            HasShadow = true,
            Padding = 15,
            Margin = 10
        };

        // Defina o BindingContext do frame para o insumo
        insumoFrame.BindingContext = insumo;

        var tapGestureRecognizer = new TapGestureRecognizer
        {
            NumberOfTapsRequired = 2
        };
        tapGestureRecognizer.Tapped += OnFrameDoubleTapped;

        insumoFrame.GestureRecognizers.Add(tapGestureRecognizer);

        var stackLayout = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    Text = $"ID: {insumo.ID_Insumo}",
                    FontSize = 14,
                    TextColor = Colors.Gray
                },
                new Label
                {
                    Text = $"Nome do Insumo: {insumo.Nome_Produto}",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black
                },
                new Label
                {
                    Text = $"Unidade de Medida: {insumo.Unidade_Medida}",
                    FontSize = 14,
                    TextColor = Colors.Gray
                }
            }
        };

        insumoFrame.Content = stackLayout;
        InsumosStackLayout.Children.Add(insumoFrame); // Adiciona o frame à StackLayout (presumindo que InsumosStackLayout existe na página)
    }

    private void OnFrameDoubleTapped(object sender, EventArgs e) {
        DisplayAlert("Teste", "O evento foi acionado.", "OK");

        if (sender is Frame frame) {
            var context = frame.BindingContext;
            if (context is Insumo insumo) {
                // Armazenar o ID do insumo selecionado
                InsumoSelecionado.ID = insumo.ID_Insumo;

                // Alterar a cor de fundo do frame
                frame.BackgroundColor = Color.FromRgb(211, 211, 211);

                // Exibir mensagem de confirmação
                DisplayAlert("Insumo Selecionado", $"Insumo com ID {insumo.ID_Insumo} foi selecionado.", "OK");
            }
            else {
                DisplayAlert("Erro", "O BindingContext não é um Insumo.", "OK");
                Console.WriteLine($"Tipo do BindingContext: {context?.GetType().Name ?? "nulo"}");
            }
        }
    }

}
