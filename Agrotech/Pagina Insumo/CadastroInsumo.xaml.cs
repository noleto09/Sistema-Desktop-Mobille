using Agrotech.Pagina_Insumo;
using System.Globalization;

namespace Agrotech;

public partial class CadastroInsumo : ContentPage
{
    private string operacaoEscolhida;

    public CadastroInsumo()
    {
        InitializeComponent();
    }

    private void Button_Novo_Insumo(object sender, EventArgs e)
    {
        try
        {
            operacaoEscolhida = "insumo";

            // Inicialize o Gerador de ID
            var geradorId = new GeradorId();

            // Gere o ID diretamente (sem verificação no banco)
            int novoIdInsumo = geradorId.GerarNovoId();

            // Atribua o ID gerado ao Entry
            Nm_Cadastro_Insumo.Text = novoIdInsumo.ToString();

            // Habilite os campos necessários para "Novo Insumo"
            Nm_Cadastro_Insumo.IsEnabled = true;
            CadastroInsumoEntry.IsEnabled = true;
            UnidadeInsumoEntry.IsEnabled = true;
           
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"Ocorreu um erro ao iniciar o insumo: {ex.Message}", "OK");
        }
    }

    private async void Button_Salvar_Insumo(object sender, EventArgs e)
    {
        try
        {
            var dataAccess = new DataAccess();

            // Obtenha os valores dos Entry para inserir na tabela de insumos
            string nomeInsumo = CadastroInsumoEntry.Text;
            string unidade = UnidadeInsumoEntry.Text;
            

            // Insira os dados na tabela de insumos
            bool insumoInserido = await dataAccess.InserirInsumoAsync(nomeInsumo, unidade);

            if (insumoInserido)
            {
                await DisplayAlert("Sucesso", "Insumo salvo com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao salvar o insumo. Verifique os dados e tente novamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

    private async void Button_Novo_Lote_Insumo(object sender, EventArgs e)
    {
        try
        {
            operacaoEscolhida = "lote";

            var geradorId = new GeradorId();
            var geradorDataHora = new GeradorDataHora(Dispatcher);

            int novoIdLote = geradorId.GerarNovoId();
            string dataAtual = geradorDataHora.DataAtual;

            Nm_Cadastro_Insumo.Text = novoIdLote.ToString();
            Datadecadastro_Insumo_Entry.Text = dataAtual;

            Nm_Cadastro_Insumo.IsEnabled = true;
            Quantidade_Entry_Insumo.IsEnabled = true;
            DatadevencimentoEntry_insumo.IsEnabled = true;
            IntercorrenciaEditor_Insumo.IsEnabled = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao iniciar o lote: {ex.Message}", "OK");
        }
    }

    private async void Button_Selecionar_Insumo(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelecionarInsumo());
    }

    private async void Button_Salvar_Lote_Insumo(object sender, EventArgs e)
    {
        try
        {
            int idInsumoSelecionado = InsumoSelecionado.ID;

            if (idInsumoSelecionado == 0)
            {
                await DisplayAlert("Erro", "Nenhum insumo foi selecionado. Por favor, selecione um insumo antes de salvar o lote.", "OK");
                return;
            }

            int quantidadeInsumo = int.Parse(Quantidade_Entry_Insumo.Text);
            DateTime dataVencimentoInsumo = DateTime.Parse(DatadevencimentoEntry_insumo.Text);
            int nLoteInsumo = int.Parse(Nm_Cadastro_Insumo.Text);
            DateTime dataCadastroInsumo = DateTime.ParseExact(Datadecadastro_Insumo_Entry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string intercorrenciaInsumo = IntercorrenciaEditor_Insumo.Text;

            var dataAccess = new DataAccess();
            bool loteInseridoInsumo = await dataAccess.InserirLoteInsumoAsync(idInsumoSelecionado, nLoteInsumo, quantidadeInsumo, dataCadastroInsumo, dataVencimentoInsumo, intercorrenciaInsumo);

            if (loteInseridoInsumo)
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
