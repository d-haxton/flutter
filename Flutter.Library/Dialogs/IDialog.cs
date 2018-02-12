using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.Library.Dialogs
{
    public interface IDialog
    {
        Task ShowMessageAsync(string message);
    }
}
