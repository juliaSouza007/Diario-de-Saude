using DiariodeSaude.ViewsModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<RegistroCompletoDTO> TodosRegistros { get; set; } = new();

        private ObservableCollection<RegistroCompletoDTO> _registrosFiltrados = new();
        public ObservableCollection<RegistroCompletoDTO> RegistrosFiltrados
        {
            get => _registrosFiltrados;
            set
            {
                _registrosFiltrados = value;
                OnPropertyChanged(nameof(RegistrosFiltrados));
            }
        }

        public List<string> OpcoesPeriodo { get; } = new()
        {
            "Todos os registros",
            "Última semana",
            "Último mês",
            "Último ano"
        };

        private string _periodoSelecionado = "Último mês";

        public string PeriodoSelecionado
        {
            get => _periodoSelecionado;
            set
            {
                _periodoSelecionado = value;
                OnPropertyChanged(nameof(PeriodoSelecionado));
                AplicarFiltro();
            }
        }

        public async Task CarregarRegistrosAsync()
        {
            try
            {
                var linq = new RegistroDiarioLinq();
                var lista = await linq.ObterRegistrosCompletosAsync();

                TodosRegistros = lista;
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AplicarFiltro()
        {
            if (PeriodoSelecionado == "Todos os registros")
            {
                RegistrosFiltrados = new ObservableCollection<RegistroCompletoDTO>(
                    TodosRegistros.OrderByDescending(r => r.Data)
                );
                return;
            }

            DateTime inicio, fim;

            switch (PeriodoSelecionado)
            {
                case "Última semana":
                    inicio = DateTime.Today.AddDays(-7);
                    fim = DateTime.Today;
                    break;
                case "Último mês":
                    inicio = DateTime.Today.AddMonths(-1);
                    fim = DateTime.Today;
                    break;
                case "Último ano":
                    inicio = DateTime.Today.AddYears(-1);
                    fim = DateTime.Today;
                    break;
                default:
                    inicio = DateTime.MinValue;
                    fim = DateTime.MaxValue;
                    break;
            }

            var filtrados = TodosRegistros
                .Where(r => r.Data.HasValue && r.Data.Value.Date >= inicio.Date && r.Data.Value.Date <= fim.Date)
                .OrderByDescending(r => r.Data)
                .ToList();

            RegistrosFiltrados = new ObservableCollection<RegistroCompletoDTO>(filtrados);
        }
    }
}