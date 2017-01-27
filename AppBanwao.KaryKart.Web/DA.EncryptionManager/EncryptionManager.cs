using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DA.EncryptionManager
{
    public class EncryptionManager
    {
        public static SecureString EncryptData(string passStringtoEncrypted)
        {
            SecureString strEncrypted = new SecureString();
            string pwd = null;
            int i;
            byte[] iKeyChar = Encoding.UTF8.GetBytes(passStringtoEncrypted);
            int iCryptChar;
            byte keychar;
            for (i = 0; i < passStringtoEncrypted.Length; i++)
            {

                keychar = iKeyChar[i];
                iCryptChar = keychar + 3;
                pwd = pwd + (char)iCryptChar;
            }
            
            char[] pwdarray = (pwd != null) ? pwd.ToCharArray() : null;
            
           
            if (pwdarray != null)
            {
           
                foreach (char c in pwdarray)
                {
                    strEncrypted.AppendChar(c);
                }
            }
            return (strEncrypted);
        }

        public static SecureString EncryptData(string passStringtoEncrypted, bool UseReverse)
        {
            SecureString strEncrypted = new SecureString();
            string pwd = null;
            int i;
            byte[] iKeyChar = Encoding.UTF8.GetBytes(passStringtoEncrypted);
            int iCryptChar;
            byte keychar;
            for (i = 0; i < passStringtoEncrypted.Length; i++)
            {

                keychar = iKeyChar[i];
                iCryptChar = keychar + 3;
                pwd = pwd + (char)iCryptChar;
            }
            
            char[] pwdarray = (pwd != null) ? pwd.ToCharArray() : null;
            
           
            if (pwdarray != null)
            {
                if(UseReverse)
                foreach (char c in pwdarray.Reverse())
                {
                    strEncrypted.AppendChar(c);
                }
            }
            return (strEncrypted);
        }

        public static string ConvertToUnSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {

                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                string pdec = "";
                for (int i = 0; i < secstrPassword.Length; i++)
                {
                    Char ch = (Char)Marshal.ReadInt16(unmanagedString, i * 2);
                    pdec = pdec + ch;
                }
                return (pdec);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString DecryptData(string passStringtoEncrypted)
        {
            SecureString strEncrypted = new SecureString();
            string pwd = null;
            int i;
            byte[] iKeyChar = Encoding.UTF8.GetBytes(passStringtoEncrypted);
            int iCryptChar;
            byte keychar;
            for (i = 0; i < passStringtoEncrypted.Length; i++)
            {

                keychar = iKeyChar[i];

                iCryptChar = keychar - 3;
                pwd = pwd + (char)iCryptChar;

            }

            char[] pwdarray = (pwd != null) ? pwd.ToCharArray() : null;
            if (pwdarray != null)
            {
                foreach (char c in pwdarray)
                {
                    strEncrypted.AppendChar(c);
                }
            }
            return (strEncrypted);
        }
        
        public static SecureString DecryptData(string passStringtoEncrypted, bool UseReverse)
        {
            SecureString strEncrypted = new SecureString();
            string pwd = null;
            int i;
            byte[] iKeyChar = Encoding.UTF8.GetBytes(passStringtoEncrypted);
            int iCryptChar;
            byte keychar;
            for (i = 0; i < passStringtoEncrypted.Length; i++)
            {

                keychar = iKeyChar[i];

                iCryptChar = keychar - 3;
                pwd = pwd + (char)iCryptChar;

            }

            char[] pwdarray = (pwd != null) ? pwd.ToCharArray() : null;
            if (pwdarray != null)
            {
                if(UseReverse)
                foreach (char c in pwdarray.Reverse())
                {
                    strEncrypted.AppendChar(c);
                }
            }
            return (strEncrypted);
        }
   
    }
}
