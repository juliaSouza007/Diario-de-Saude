using System;
using System.Windows.Input;
using Avalonia.Controls;
using DiariodeSaude.ViewsModels;
using System.Collections.ObjectModel;

namespace DiariodeSaude.ViewModels
{
    public class EditarRegistroViewModel : ViewModelBase
    {
        private RegistroCompletoDTO? _registroSelecionado;

        public RegistroCompletoDTO? RegistroSelecionado
        {
            get => _registroSelecionado;
            set => SetProperty(ref _registroSelecionado, value);
        }

        private string? _humor;
        public string? Humor
        {
            get => _humor;
            set => SetProperty(ref _humor, value);
        }

        private string? _sono;
        public string? Sono
        {
            get => _sono;
            set => SetProperty(ref _sono, value);
        }

        private string? _alimentacao;
        public string? Alimentacao
        {
            get => _alimentacao;
            set => SetProperty(ref _alimentacao, value);
        }

        private string? _atividadeFisica;
        public string? AtividadeFisica
        {
            get => _atividadeFisica;
            set => SetProperty(ref _atividadeFisica, value);
        }

        private string? _tempo;
        public string? Tempo
        {
            get => _tempo;
            set => SetProperty(ref _tempo, value);
        }

        private DateTime? _data;
        public DateTime? Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public EditarRegistroViewModel(RegistroCompletoDTO registro)
        {
            RegistroSelecionado = registro;
            Humor = registro.Humor;
            Sono = registro.Sono.ToString();
            Alimentacao = registro.Alimentacao;
            AtividadeFisica = registro.Atividade;
            Tempo = registro.Tempo.ToString();
        }

        public ICommand SalvarCommand => new RelayCommand(Salvar);

        private void Salvar()
        {
            if (RegistroSelecionado != null)
            {
                // Aqui você atualiza o registro com os valores editados
                RegistroSelecionado.Humor = Humor;
                RegistroSelecionado.Sono = int.TryParse(Sono, out var sonoHoras) ? sonoHoras : 0;
                RegistroSelecionado.Alimentacao = Alimentacao;
                RegistroSelecionado.Atividade = AtividadeFisica;
                RegistroSelecionado.Tempo = int.TryParse(Tempo, out var tempoMin) ? tempoMin : 0;
                RegistroSelecionado.Data = Data;
            }
        }
    }

    // Classe simples para comandos (substituto de ReactiveCommand)
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (() => true); // Fornecendo um valor padrão
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        public void Execute(object? parameter) => _execute();

        public event EventHandler? CanExecuteChanged;
    }
}
