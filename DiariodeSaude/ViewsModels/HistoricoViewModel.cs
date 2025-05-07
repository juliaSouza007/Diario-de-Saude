using DiariodeSaude.ViewsModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System;

namespace DiariodeSaude.ViewsModels
{
    public class HistoricoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RegistroCompletoDTO> _registros = new();

        public ObservableCollection<RegistroCompletoDTO> Registros
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
            try
            {
                var registroLinq = new RegistroDiarioLinq();
                var lista = await registroLinq.ObterRegistrosCompletosAsync();

                Registros = new ObservableCollection<RegistroCompletoDTO>(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
