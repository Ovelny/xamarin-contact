using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp
{
    public interface ICellPhone
    {
        void OpenSMS(string PhoneNumber);
        void CallContact(string PhoneNumber);
        void SelectImageFromGallery();
        void TakePicture();
    }
}
