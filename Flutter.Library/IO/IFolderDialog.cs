using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.Library.IO
{
    public interface IFolderDialog
    {
        string SelectFolder(string description);
    }
}
