using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace museum
{
    public partial class Login : Form
    {
        UserDAO userDao = new UserDAO();

        private static Login instance = null;

        public static Login getInstance()
        {
            if(instance == null)
            {
                instance  = new Login();
                instance.FormClosed += delegate { instance = null; };
            }
            return instance;
        }

        private Login()
        {
            InitializeComponent();
        }

        private void okButtonLoginForm_Click(object sender, EventArgs e)
        {
            string username = textBoxUserLoginForm.Text;
            string password = textBoxPasswordLoginForm.Text;

            /*Here goes the validation*/
            if (username == "" || password == "")
            {
                MessageBox.Show("Please input all requested info");
                return;
            }

            List<User> userList = userDao.getAllUserData();

            bool found = false;
            foreach(User user in userList)
            {
                if (user.username == username && user.password == password)
                {
                    /*we place the current user's object into a global variable of type User, so we can access it from all the forms*/
                    Globals.currentUser = user;
                    found = true;
                    break;
                }
                
            
            }
            /*in case the user is not found*/
            if(found==false)
            {
                MessageBox.Show("Unknown username or wrong password.");
                return;
            }



           /*change the text of comboBox at the main form, by adding the username*/
            //FormCollection forms = Application.OpenForms;


            /*Done the job and now fires the event closes*/
            OnUserLoggedIn();
            MessageBox.Show("Successful login!");
            instance = null;
            this.Close();
        }

        public delegate void OnLoggedInEventHandler(object sender,EventArgs e);

        public event OnLoggedInEventHandler LoggedIn;

        public void OnUserLoggedIn() {
            //fires the event
            if(LoggedIn!= null)
                LoggedIn(this,null);
        }


    }
}
