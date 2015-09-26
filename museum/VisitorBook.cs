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
    public partial class VisitorBook : Form
    {
        private static VisitorBook visitorBookInstance;

        public static VisitorBook getVisitorBookInstance()
        {
            if(visitorBookInstance == null)
            {
                visitorBookInstance = new VisitorBook();
                visitorBookInstance.FormClosed += delegate { visitorBookInstance =null;};
            }
            return visitorBookInstance;
        }

        VisitorBooksEntryDAO visitorDao = new VisitorBooksEntryDAO();
        List<VisitorBooksEntry> visitorBookList;

        public VisitorBook()
        {
            InitializeComponent();
            visitorBookList = visitorDao.getAllVisitorsBookData();

            /*for every row in the database produce a new groupbox, along with its inner controls*/
            foreach(VisitorBooksEntry vbe in visitorBookList)
            {
                dynamicallyCreateControl(vbe);
            
            }

        }

        private void messageGroupBox_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is VisitorBook)
                splitContainer1.Panel1.Focus();
        }

        private void visitorBookButtonSend_Click(object sender, EventArgs e)
        {
            var message = visitorMessageBox.Text;
            var entry = new VisitorBooksEntry();
            entry.text = message;
            entry.name = Globals.currentUser.name + " " + Globals.currentUser.last;
            if (visitorDao.addVisitorMessage(entry))
            {
                MessageBox.Show("Message entered successfully!");
                /*load the message in the upper part of the form*/

                dynamicallyCreateControl(entry);
            
            }
        }


        private void dynamicallyCreateControl(VisitorBooksEntry vbe){
        /*we build in the designer the prototype, then we copy it here since we want this to be created dynamically and then we erase the prototype*/
                GroupBox messageGroupBox = new GroupBox();
                Label visitorsBookNameTag = new Label();
                Label visitorsBookMessageTag = new Label();
                Label visitorsBookMessage = new Label();
                Label visitorsBookName = new Label();
                messageGroupBox.AutoSize = true;
                messageGroupBox.Controls.Add(visitorsBookNameTag);
                messageGroupBox.Controls.Add(visitorsBookMessage);
                messageGroupBox.Controls.Add(visitorsBookMessageTag);
                messageGroupBox.Controls.Add(visitorsBookName);

                //splitContainer1.BackColor = System.Drawing.Color.Gainsboro;
                messageGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
                splitContainer1.Panel1.Controls.Add(messageGroupBox);

                messageGroupBox.Name = "messageGroupBox";
                messageGroupBox.Size = new System.Drawing.Size(1023, 86);
                messageGroupBox.TabIndex = 4;
                messageGroupBox.TabStop = false;
                messageGroupBox.Text = "Μήνυμα Επισκέπτη";
                messageGroupBox.MouseEnter += new System.EventHandler(this.messageGroupBox_MouseEnter);

                // 
                // visitorsBookNameTag
                // 
                visitorsBookNameTag.AutoSize = true;
                visitorsBookNameTag.Location = new System.Drawing.Point(29, 33);
                visitorsBookNameTag.Name = "visitorsBookNameTag";
                visitorsBookNameTag.Size = new System.Drawing.Size(38, 13);
                visitorsBookNameTag.TabIndex = 0;
                visitorsBookNameTag.Text = "Name:";
                // 
                // visitorsBookMessageTag
                // 
                visitorsBookMessageTag.AutoSize = true;
                visitorsBookMessageTag.Location = new System.Drawing.Point(29, 57);
                visitorsBookMessageTag.Name = "visitorsBookMessageTag";
                visitorsBookMessageTag.Size = new System.Drawing.Size(56, 13);
                visitorsBookMessageTag.TabIndex = 1;
                visitorsBookMessageTag.Text = "Message :";
                // 
                // visitorsBookName
                // 
                visitorsBookName.AutoSize = true;
                visitorsBookName.Location = new System.Drawing.Point(106, 33);
                visitorsBookName.Name = "visitorsBookName";
                visitorsBookName.Size = new System.Drawing.Size(35, 13);
                visitorsBookName.TabIndex = 2;
                visitorsBookName.Text = vbe.name;
                visitorsBookName.MaximumSize = new Size(900, 0);
                // 
                // visitorsBookMessage
                // 
                visitorsBookMessage.AutoSize = true;
                visitorsBookMessage.Location = new System.Drawing.Point(106, 57);
                visitorsBookMessage.Name = "visitorsBookMessage";
                visitorsBookMessage.Size = new System.Drawing.Size(35, 13);
                visitorsBookMessage.TabIndex = 3;
                visitorsBookMessage.Text = vbe.text;
                visitorsBookMessage.MaximumSize = new Size(900, 0);

        }


    }
}
