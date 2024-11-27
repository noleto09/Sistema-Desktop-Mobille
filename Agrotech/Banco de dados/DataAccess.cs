using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class DataAccess
{
    private readonly string connStr = "server=10.0.2.2;user=root;database=sys_agrotech;port=3306;password=AGROtech78@%24";

    public async Task<List<Produto>> GetAllProdutosAsync()
    {
        var produtos = new List<Produto>();
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "SELECT ID_Produto, Nome_Produto, Unidade_Medida, Valor FROM Produtos";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            produtos.Add(new Produto
                            {
                                ID_Produto = reader.GetInt32("ID_Produto"),
                                Nome_Produto = reader.GetString("Nome_Produto"),
                                Unidade_Medida = reader.GetString("Unidade_Medida"),
                                Valor = reader.GetDecimal("Valor")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
        return produtos;
    }

    public async Task<bool> InserirLoteAsync(int idProduto, int nLote, int quantidade, DateTime dataCadastro, DateTime dataVencimento, string intercorrencia)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();
                string sql = "INSERT INTO Lotes (ID_Produto, N_Lote, Data_Cadastro, Quantidade, Data_Vencimento, Intercorrencia_lote) " +
                    "VALUES (@idProduto, @nLote, @dataCadastro, @quantidade, @dataVencimento, @intercorrencia)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduto", idProduto);
                    cmd.Parameters.AddWithValue("@nLote", nLote);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@dataCadastro", dataCadastro);
                    cmd.Parameters.AddWithValue("@dataVencimento", dataVencimento);
                    cmd.Parameters.AddWithValue("@intercorrencia", intercorrencia);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inserir lote: " + ex.Message);
            return false;
        }
    }
    public async Task<string> VerificarUsuarioAsync(string nomeUsuario, string senha)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "SELECT nome_usuario FROM usuario WHERE nome_usuario = @nome_usuario AND senha_usuario = @senha";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome_usuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    var result = await cmd.ExecuteScalarAsync();
                    return result?.ToString(); // Retorna o nome do usuário ou null se não encontrado
                }
            }
        }
        catch (Exception ex)
        {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return null; // Retorna null em caso de erro
        }
    }


    public async Task<bool> InserirInsumoAsync(string nomeProduto, string unidade)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "INSERT INTO Insumos (Nome_Produto, Unidade_Medida) VALUES (@nomeProduto, @unidade)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                    cmd.Parameters.AddWithValue("@unidade", unidade);
                    

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<List<Insumo>> GetAllInsumosAsync()
    {
        var insumos = new List<Insumo>();
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "SELECT ID_Insumo, Nome_Produto, Unidade_Medida FROM Insumos"; // Ajuste na tabela e nos campos
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            insumos.Add(new Insumo
                            {
                                ID_Insumo = reader.GetInt32("ID_Insumo"),
                                Nome_Produto = reader.GetString("Nome_Produto"),
                                Unidade_Medida = reader.GetString("Unidade_Medida"),
                               
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
        return insumos;
    }

    public async Task<bool> InserirLoteInsumoAsync(int idProduto, int nLoteInsumo, int quantidadeInsumo, DateTime dataCadastroInsumo, DateTime dataVencimentoInsumo, string intercorrenciaInsumo) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                string sql = "INSERT INTO Lotes_Insumos (ID_Insumo, N_Lote, Data_Cadastro, Quantidade, Data_Vencimento, Intercorrencia_lote) " +
                    "VALUES (@idInsumo, @nLote, @dataCadastro, @quantidade, @dataVencimento, @intercorrencia)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@idProduto", idProduto);
                    cmd.Parameters.AddWithValue("@nLote", nLoteInsumo);
                    cmd.Parameters.AddWithValue("@quantidade", quantidadeInsumo);
                    cmd.Parameters.AddWithValue("@dataCadastro", dataCadastroInsumo);
                    cmd.Parameters.AddWithValue("@dataVencimento", dataVencimentoInsumo);
                    cmd.Parameters.AddWithValue("@intercorrencia", intercorrenciaInsumo);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao inserir lote: " + ex.Message);
            return false;
        }
    }

}
