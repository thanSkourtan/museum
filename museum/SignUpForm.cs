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
    public partial class SignUpForm : Form
    {
        UserDAO userDao = new UserDAO();
        int userId;
        User user;

        private static SignUpForm signUpInstance;

        public static SignUpForm getSignUpInstance(int userId)
        {
            if (signUpInstance == null)
            { 
                signUpInstance = new SignUpForm(userId);
                signUpInstance.FormClosed += delegate { signUpInstance = null;  };
            }
            return signUpInstance;
        }


        private SignUpForm(int userId)
        {
            this.userId = userId;

            
            InitializeComponent();

            /*if userId is not 0 use the form as edit form and load the user data*/
            if (userId != 0)
            {
                user = userDao.getSingleUser(userId);
                SignUpEmail.Text = user.email;
                SignUpLast.Text = user.last;
                SignUpPassword.Text = user.password;
                SignUpUserName.Text = user.username;
                SignUpName.Text = user.name;
                   
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*use the form as an sign up form*/
            if (userId == 0)
            {
                /*here goes the validation*/
                var username = SignUpUserName.Text;
                var password = SignUpPassword.Text;
                var name = SignUpName.Text;
                var last = SignUpLast.Text;
                var email = SignUpEmail.Text;
                if (username == "" || password == "" || name == "" || last == "" || email == "")
                {
                    MessageBox.Show("Please fill in all fields!");
                    return;
                }

                User newUser = new User();
                newUser.name = name;
                newUser.username = username;
                newUser.last = last;
                newUser.password = password;
                newUser.email = email;

              

                if (userDao.addUser(newUser))
                {
                    MessageBox.Show("User entered!");
                    newUser.id = userDao.getLastUser().id;

                    Globals.currentUser = newUser;

                    /*Fire the UserSignedUp event in order to fill in the combo box in main form*/
                    OnSignUp();
                    this.Close();

                }

            }
            /*use the form as an editform*/
            else 
            {
                /*here goes the validation*/
                var username = SignUpUserName.Text;
                var password = SignUpPassword.Text;
                var name = SignUpName.Text;
                var last = SignUpLast.Text;
                var email = SignUpEmail.Text;
                if (username == "" || password == "" || name == "" || last == "" || email == "")
                {
                    MessageBox.Show("Please fill in all fields!");
                    return;
                }

                User newUser = new User();
                newUser.id = userId;
                newUser.name = name;
                newUser.username = username;
                newUser.last = last;
                newUser.password = password;
                newUser.email = email;

                if(userDao.updateUser(newUser))
                {
                    MessageBox.Show("User updated!");
                    Globals.currentUser = newUser;

                    /*Fire the UserSignedUp event in order to fill in the combo box in main form*/
                    OnSignUp();
                    this.Close();
                }


                

            }

        }
    


        public delegate void UserSignedUpEventHandler(object sender,EventArgs e);
        public event UserSignedUpEventHandler UserSignedUp;


        public void OnSignUp() {
            if (UserSignedUp != null)
            {
                UserSignedUp(this, null);
            }
        
        }

    }
}
