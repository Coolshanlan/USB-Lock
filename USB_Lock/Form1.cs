using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
namespace USB_Lock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = MD5.Create(); 
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));
        return sb.ToString();
    }
     Password p = new Password();
     string pass = "99EDB8557AE14E30409A5CFDBE2D7CCE";
     private void button1_Click(object sender, EventArgs e)
     {
         p.ShowDialog();
         if (GetHashString(p.password) == pass)
         {
             if (checkBox1.Checked)
             {
                 Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 4, Microsoft.Win32.RegistryValueKind.DWord);
                }
             else
             {
                 Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3, Microsoft.Win32.RegistryValueKind.DWord);
             }
                MessageBox.Show("Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed");
            }
     }

        private void Form1_Load(object sender, EventArgs e)
        {
            var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", null);
            if (value.ToString() == "3") checkBox1.Checked = false;
            else checkBox1.Checked = true;
        }
    }
}
