using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.SecsGem.App.ViewModels
{
    internal class SmlTransformerViewModel: BindableBase
    {
        private string _byteText;

        public SmlTransformerViewModel()
        {
            ClearByteTextCommand = new DelegateCommand(ClearByteText);
        }

        private void ClearByteText()
        {
            ByteText = string.Empty;
        }

        public DelegateCommand<string> CopyByteTextCommand { get; private set; }
        public DelegateCommand ClearByteTextCommand { get; private set; }

        public string ByteText
        {
            get => _byteText;
            set => SetProperty(ref _byteText, value);
        }
    }
}
