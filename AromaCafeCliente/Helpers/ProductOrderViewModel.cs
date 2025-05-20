using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AromaCafeCliente.Helpers
{
    public class ProductOrderViewModel : INotifyPropertyChanged
    {
        public string Producto { get; }
        public int Cantidad { get; }

        public decimal Precio { get; }

        private bool _seleccionado;
        public bool Seleccionado
        {
            get => _seleccionado;
            set
            {
                if (_seleccionado != value)
                {
                    _seleccionado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seleccionado)));
                }
            }
        }

        public ProductOrderViewModel(string producto, int cantidad, string orderState)
        {
            Producto = producto;
            Cantidad = cantidad;
            _seleccionado = orderState == "Entregado";
        }

        public ProductOrderViewModel(string producto, int cantidad, decimal precio)
        {
            Producto = producto;
            Cantidad = cantidad;
            Precio = precio;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
