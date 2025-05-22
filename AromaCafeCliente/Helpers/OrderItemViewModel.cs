using System.ComponentModel;

namespace AromaCafeCliente.Helpers
{
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        public string Producto { get; }
        public decimal PrecioUnitario { get; }

        private int _cantidad;
        public int Cantidad
        {
            get => _cantidad;
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    OnPropertyChanged(nameof(Cantidad));
                    OnPropertyChanged(nameof(PrecioTotal));
                }
            }
        }

        public decimal PrecioTotal => PrecioUnitario * Cantidad;

        public OrderItemViewModel(string producto, int cantidad, decimal precioUnitario)
        {
            Producto = producto;
            PrecioUnitario = precioUnitario;
            _cantidad = cantidad;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
