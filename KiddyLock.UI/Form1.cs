using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Core.Models;
using KiddyLock.UI.Controls;
using KiddyLock.UI.Logic;

namespace KiddyLock.UI
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Closed += Form1_Closed;
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            
        }

      

       
        private async void GetUsers()
        {
            _osUsers = UserLogic.GetOsUsers();
            _users = await UserLogic.GetUsers();

            var userBinding = new BindingList<User>();
            foreach (var user in _osUsers)
            {
                userBinding.Add(user);
            }
            UserCombo.DataSource = userBinding;
            UserCombo.DisplayMember = "Name";
            UserCombo.ValueMember = "Sid";
        }

        private void UserCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            _selectedUser = _osUsers[comboBox.SelectedIndex];
        }

        private void UsersButton_Click(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
