using System;
using System.Threading.Tasks;

public class GeradorId
{
    public GeradorId()
    {
        // O construtor agora não precisa mais de DataAccess
    }

    // Método para gerar um novo ID aleatório (sem verificar no banco de dados)
    public int GerarNovoId()
    {
        Random random = new Random();
        int randomId = random.Next(100000, 999999); // ID aleatório de 6 dígitos
        return randomId;
    }
}
