using System.Globalization;

namespace Agrotech;

public partial class CadastroProduto : ContentPage
{
    private string operacaoEscolhida;

    public CadastroProduto()
    {
        InitializeComponent();
    }

    private async void Button_Novo_Lote(object sender, EventArgs e)
    {
        try
        {
            operacaoEscolhida = "lote";

            // Inicialize o Gerador de ID e Data
            var geradorId = new GeradorId();
            var geradorDataHora = new GeradorDataHora(Dispatcher);

            // Gere o ID diretamente (sem verificação no banco)
            int novoIdLote = geradorId.GerarNovoId();

            // Obtenha a data atual do Gerador de Data
            string dataAtual = geradorDataHora.DataAtual;

            // Atribua o ID gerado ao Entry
            Nm_Cadastro_Produto_Insumo.Text = novoIdLote.ToString();

            // Atribua a data ao campo de data
            DatadecadastroEntry.Text = dataAtual;

            // Habilite os campos necessários
            Nm_Cadastro_Produto_Insumo.IsEnabled = true;
            Quantidade_Entry_PI.IsEnabled = true;
            DatadevencimentoEntry.IsEnabled = true;
            IntercorrenciaEditor.IsEnabled = true;
        }
        catch (Exception ex)
        {
            // Exibir uma mensagem de erro para identificar o problema
            await DisplayAlert("Erro", $"Ocorreu um erro ao iniciar o lote: {ex.Message}", "OK");
        }
    }

    private async void Button_Selecionar_Produto(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarProduto());
    }

    private async void Button_Salvar_Lote(object sender, EventArgs e)
    {
        try
        {
            // Verifique se um produto foi selecionado
            int idProdutoSelecionado = ProdutoSelecionado.ID;

            if (idProdutoSelecionado == 0)
            {
                await DisplayAlert("Erro", "Nenhum produto foi selecionado. Por favor, selecione um produto antes de salvar o lote.", "OK");
                return;
            }

            // Obtenha os valores dos Entry para inserir no lote
            int quantidade = int.Parse(Quantidade_Entry_PI.Text);
            DateTime dataVencimento = DateTime.Parse(DatadevencimentoEntry.Text);

            // Obtenha o número do lote (N_Lote)
            int nLote = int.Parse(Nm_Cadastro_Produto_Insumo.Text); // Certifique-se de que NLoteEntry é o nome do seu Entry para o número do lote

            // Obtenha a data de cadastro a partir de um Entry
            DateTime dataCadastro = DateTime.ParseExact(DatadecadastroEntry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            // Obtenha a intercorrência (se houver)
            string intercorrencia = IntercorrenciaEditor.Text; // Certifique-se de que IntercorrenciaEntry é o nome do seu Entry para intercorrência

            // Crie uma instância de DataAccess
            var dataAccess = new DataAccess();

            // Insira os dados na tabela de lotes
            bool loteInserido = await dataAccess.InserirLoteAsync(idProdutoSelecionado, nLote, quantidade, dataCadastro, dataVencimento, intercorrencia);

            if (loteInserido)
            {
                await DisplayAlert("Sucesso", "Lote salvo com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao salvar o lote. Verifique os dados e tente novamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
