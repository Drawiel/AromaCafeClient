using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AromaCafeCliente.Helpers
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public string Producto { get; }
        public decimal? Precio { get; set; }

        private bool _seleccionado;

        public bool Seleccionado
        {
            get => _seleccionado;
            set
            {
                if (_seleccionado != value)
                {
                    _seleccionado = value;
                    OnPropertyChanged(nameof(Seleccionado));
                }
            }
        }

        public ProductViewModel(string producto, decimal? precio)
        {
            Producto = producto;
            Precio = precio;
            _seleccionado = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

}
