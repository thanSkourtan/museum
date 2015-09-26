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

   
    public partial class SessionOptions : Form
    {
        
        SessionListObjectDAO dao = new SessionListObjectDAO();

        public SessionOptions()
        {
            InitializeComponent();

            List<SessionListObject> mySessionListObject = dao.getAllSessionListsForUser(Globals.currentUser.id);

            foreach(SessionListObject oe in mySessionListObject)
            {
                LinkLabel linkLabel = new LinkLabel();

                linkLabel.AutoSize = true;
                linkLabel.Location = new System.Drawing.Point(0, 10);
                linkLabel.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
                linkLabel.Name = "linkLabel-"+oe.id;
                linkLabel.Size = new System.Drawing.Size(55, 13);
                linkLabel.TabIndex = 0;
                linkLabel.TabStop = true;
                linkLabel.Text = oe.sessionName;
                linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
                flowLayoutPanel1.Controls.Add(linkLabel);
            }
            
        }

        //create a delegate
        public delegate void OnSessionChosenEventHandler(object sender, SessionEventArgs args);

        public event OnSessionChosenEventHandler SessionChosen;

        public void OnSessionChosen(SessionListObject slo) 
        {
            //raises the event
            if (SessionChosen != null)
            {
                SessionChosen(this, new SessionEventArgs() { sessionListObject=slo});
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int sessionId = Int32.Parse(((LinkLabel)sender).Name.Split('-').Last());
            SessionListObject slo = dao.getSingleSessionListObject(sessionId);
            //raises the SessionChosen event
            OnSessionChosen(slo);
            this.Close();
        }
    }

    public class SessionEventArgs : EventArgs
    {
        public SessionListObject sessionListObject { get; set; }



    }
}


