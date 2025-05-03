using DiariodeSaude.ViewsModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System;

namespace DiariodeSaude.ViewsModels
{
    public class HistoricoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<QualidadeSono> _registros = new();

        public ObservableCollection<QualidadeSono> Registros
        {
            get => _registros;
            set
            {
                _registros = value;
                OnPropertyChanged(nameof(Registros));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task CarregarRegistrosAsync()
        {
            Console.WriteLine(">>> Entrou em CarregarRegistrosAsync");

            try
            {
                var sonoLinq = new SonoLinq();
                var lista = await sonoLinq.ObterQualidadeSonoAsync();

                Console.WriteLine($">>> Registros carregados: {lista.Count}");

                Registros = new ObservableCollection<QualidadeSono>(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> ERRO AO CARREGAR REGISTROS:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
