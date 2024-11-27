using System;
using Microsoft.Maui.Dispatching;
using System.Timers;

public class GeradorDataHora
{
    private readonly IDispatcher dispatcher;
    private readonly System.Timers.Timer timer;

    public string DataAtual { get; private set; }
    public string HoraAtual { get; private set; }

    public event Action<string, string> DataHoraAtualizada;

    public GeradorDataHora(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;

        timer = new System.Timers.Timer(1000); // Atualiza a cada 1 segundo
        timer.Elapsed += AtualizarDataHora;
        timer.Start();

        AtualizarDataHora(null, null); // Atualiza imediatamente
    }

    private void AtualizarDataHora(object sender, ElapsedEventArgs e)
    {
        DateTime now = DateTime.Now;
        DataAtual = now.ToString("dd/MM/yyyy");
        HoraAtual = now.ToString("HH:mm:ss");

        dispatcher.Dispatch(() =>
        {
            DataHoraAtualizada?.Invoke(DataAtual, HoraAtual);
        });
    }

    public void PararAtualizacao()
    {
        timer.Stop();
        timer.Dispose();
    }
}
