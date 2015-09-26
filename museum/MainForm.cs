using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace museum
{
    public partial class MainForm : Form
    {
        /**********************************************field declaration******************************************************************/
        SoundPlayer player = new SoundPlayer(Properties.Resources.back_sound);

        /*we get all the Dao objects which will use to make queries to the database*/
        CategoryDAO categoryDAO = new CategoryDAO();
        ExhibitDAO dao = new ExhibitDAO();
        CalendarDAO calendarDAO = new CalendarDAO();

        /*We use it in more than two functions, so we place it here */
        List<Calendar> calendarList = new List<Calendar>();

        List<Panel> panel = new List<Panel>();
        List<PictureBox> pictureBox = new List<PictureBox>();
        List<LinkLabel> picturesLinkLabel = new List<LinkLabel>();
       
        List<LinkLabel> innerLinkLabel = new List<LinkLabel>();

        /*Every time the user presses a link in the navigation panel this current link has to have a colourful background in order to indicate in which section of the
         museum the user is. In order to achieve this, we paint the current linkLabel, however, we also have to unpaint the previous link. this is why we keep a reference to 
         it by using the below variable*/
        LinkLabel previousLink;


        //a "switch" array to check whether each of the general categories has been expanded or not
        bool[] genCategorySwitch;


        public MainForm()
        {
            InitializeComponent();
            
            /******************do some designing that has to be done dynamically************************************************************/
            stylingInTheInitialization();

            /******************fill in the slide show's image list with all the available images********************************************/
            fillInSlideShowImageList();

            /*************************************fill in the general Category Link Labels int the navigation panel*************************/
            fillInGeneralCategoryNavigationPanel();

            /*************************************fill in the calendar with the available events********************************************/
            populateCalendar();
        }

        private void stylingInTheInitialization()
        {
            TotalContainer.Panel1.Controls.Add(rightPanel);
            TotalContainer.Panel2.Controls.Add(splitContainer1);
            rightPanel.Dock = DockStyle.Fill;
            splitContainer1.Dock = DockStyle.Fill;

            //set the text of the main Label when opening the application
            this.mainLabelText.Text = Properties.Resources.init_text;

            slideShowPlay.Image = Properties.Resources.play_normal1.ToBitmap();
            slideShowPause.Image = Properties.Resources.Pause_Normal.ToBitmap();
            slideShowStop.Image = Properties.Resources.Stop_Normal_Blue.ToBitmap();

            multimediaPlay.Image = Properties.Resources.play_normal1.ToBitmap();
            multimediaStop.Image = Properties.Resources.Stop_Normal_Blue.ToBitmap();
            //some features of the designer stopped working due to visual studio bug, so they are set programmatically.
            picturesFlowPanel.BackgroundImage = Properties.Resources.agiasofia;
            slideShowPictureBox.BackgroundImage = Properties.Resources.museum;
            historyButton.Image = Properties.Resources.arrow_alt_left;
            //toolStripButton8.Image = Properties.Resources.arrow_alt_right;
            visitorsbookimage.Image = Properties.Resources.guestbook_icon;
            flowNavigationPanel.AutoScroll = true;

        }

        private void fillInSlideShowImageList() {
            List<Exhibit> exhibitList = dao.displayAll();

            foreach (Exhibit ex in exhibitList)
            {
                Image tempImage = Image.FromFile("..\\..\\Resources\\" + ex.image);
                //tempImage.Tag = "dfs";
                //tempImage.Tag = "" + ex.category + "-" + ex.id;
                slideShowImageList.Images.Add(tempImage);
            }

            slideShowStripLabel.Text = "0 images of " + slideShowImageList.Images.Count + ".";
        }

        private void fillInGeneralCategoryNavigationPanel()
        {
            List<GeneralCategory> genCatList = categoryDAO.getGeneralCategory();
            List<Category> catList = categoryDAO.getAllCategories();

            genCategorySwitch = new bool[genCatList.Count];

            int counter = 0;
            //two loops. One to fill in the general categories data and the second one, nested, to fill in the simple categories data
            foreach (GeneralCategory genCat in genCatList)
            {
                //counter++;
                LinkLabel tempCurrentLinkLabel = new LinkLabel();
                tempCurrentLinkLabel.Text = genCat.name;
                tempCurrentLinkLabel.AutoSize = false;
                tempCurrentLinkLabel.Width = 250;
                tempCurrentLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
                tempCurrentLinkLabel.Size = new Size(300, 0);
                tempCurrentLinkLabel.AutoSize = true;
                tempCurrentLinkLabel.Name = "GenCatLinkLabel-" + counter + "-" + genCat.id;
                tempCurrentLinkLabel.LinkColor = System.Drawing.Color.Black;
                tempCurrentLinkLabel.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
                //fix the bottom margin only at the last general category linklabel
                if (counter == genCatList.Count() - 1) tempCurrentLinkLabel.Margin = new System.Windows.Forms.Padding(16, 3, 3, 23);
                
                /*the initial page has to be added to the history objects' list, however it is not produced by a click event,that's the use of the counter. So we add it "manually"*/
                if (counter == 0)
                {
                    //sets the color of the first general category linklabel to scrollbar
                    previousLink = tempCurrentLinkLabel;
                    tempCurrentLinkLabel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    var historyObject = new HistoryObject();
                    historyObject.obj = tempCurrentLinkLabel;
                    historyObject.e = new LinkLabelLinkClickedEventArgs(null,MouseButtons.Left);
                    Globals.historyObjectList.Add(historyObject);
                }
                counter++;

                flowNavigationPanel.Controls.Add(tempCurrentLinkLabel);
                //subscribe a handler to the event LinkClicked
                tempCurrentLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);

                /*********end of single item general category input**********/

                /*we iterate ALL the categories array each time, to be sure that everything will run smoothly, that's why we use a for loop and not a while loop with a break statement*/
                for (int i = 0; i < catList.Count(); i++)
                {   //if the gen category id of the simple category is the same with the current general category
                    if (Int32.Parse(tempCurrentLinkLabel.Name.Split('-').Last()) == catList[i].generalCategory)
                    {
                        innerLinkLabel.Add(new LinkLabel());
                        LinkLabel currentItem = innerLinkLabel.Last();
                        //LinkLabel currentItem = new LinkLabel();
                        currentItem.Text = catList[i].name;
                        currentItem.AutoSize = false;
                        currentItem.Width = 250;
                        currentItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
                        currentItem.Size = new Size(300, 0);
                        currentItem.AutoSize = true;
                        currentItem.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
                        currentItem.Name = "innerLinkLabel-" + catList[i].generalCategory + "-" + catList[i].id;
                        currentItem.LinkColor = System.Drawing.Color.Black;
                        currentItem.Hide();
                        flowNavigationPanel.Controls.Add(currentItem);

                        /*the first parameter, child, has to be already a child of the flowNavigationPanel, before resetting its index*/
                        //int positionOfCorrespondingGeneralCategory = flowNavigationPanel.Controls.IndexOf((LinkLabel)sender);//gets the position of the genCategory linklabel that was clicked
                        //flowNavigationPanel.Controls.SetChildIndex(currentItem, positionOfCorrespondingGeneralCategory + 1 + j);
                        //j++;

                        //subscribe a handler to the event LinkClicked
                        currentItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.innerLinkLabel_LinkClicked); 
                    }
                }

            }
        }

        private void populateCalendar()
        {

            calendarList = calendarDAO.getAllCalendarData();

            foreach (Calendar cal in calendarList)
            {
                monthCalendar.AddBoldedDate(cal.date);
            }
        }

        /******************************************************************************************************************************** ***/
      
        /*when the user clicks on either on one of the pictures or the linklabels of the main panel, the single form appears*/
        private void picturesLinkLabel_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
            {
                ((LinkLabel)sender).LinkVisited = true;
                Globals.addHistoryObject(sender, e);
                Globals.addObject(sender, e);
                //historyObject.Add(sender);
                singleFormAppear(sender);
            }
        }

        private void MainPanelPictureBox_Click(object sender, EventArgs e)
        {
            if(((MouseEventArgs)e).Button==MouseButtons.Left)
            {
                //find the next control from the pictureBox, which is always a LinkLabel and mark it as 'visited'
                ((LinkLabel)this.GetNextControl((Control)sender, true)).LinkVisited = true;

                Globals.addHistoryObject(sender, e);
                Globals.addObject(sender, e);
                //historyObject.Add(sender);
                singleFormAppear(sender);
            }
            
        }

        private void singleFormAppear(object sender)
        {
            if(sender is PictureBox)
            {
                /*get the id from the name*/
                string pictureBoxId = ((PictureBox)sender).Name.Split('-').Last();
                /*get the category from the name*/
                string[] categoryIdString = ((PictureBox)sender).Name.Split('-');
                string categoryId = categoryIdString[categoryIdString.Length - 2];

                Form singleExhibitDisplayForm = new SingleExhibitForm(Int32.Parse(pictureBoxId), Int32.Parse(categoryId),null,null);
                singleExhibitDisplayForm.Show();
            }
            else if (sender is LinkLabel) 
            {
                /*get the id from the name*/
                string linkLabelId = ((LinkLabel)sender).Name.Split('-').Last();
                /*get the category from the name*/
                string[] categoryIdString = ((LinkLabel)sender).Name.Split('-');
                string categoryId = categoryIdString[categoryIdString.Length - 2];

                Form singleExhibitDisplayForm = new SingleExhibitForm(Int32.Parse(linkLabelId), Int32.Parse(categoryId),null,null);
                singleExhibitDisplayForm.Show();
            
            }

        }


        /*****************************makes the cursor hand, when over a picture********************************************************************************/
        
        private void MainPanelPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        /*makes the cursor default, when leaving a picture*/
        private void MainPanelPictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
    

        /*************************************************makes the panels appear/disappear***************************************************************/
        private void PanelDisplay(Panel panel, ToolStripMenuItem toolStripMenuItem) 
        {
            /*make the corresponding panel appear/disappear*/
            if (panel.Visible == false)
            {
                toolStripMenuItem.Checked = true;
                panel.Show();
            }
            else
            {
                panel.Hide();
                toolStripMenuItem.Checked = false;
            }
        }

        private void calendarPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelDisplay(calendarPanel, calendarPanelToolStripMenuItem1);
        }

        private void slideShowPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelDisplay(slideShowPanel, slideShowPanelToolStripMenuItem);
        }

        private void multimediaPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelDisplay(multimediaPanel, multimediaPanelToolStripMenuItem1);
        }
    
        /********************event Handler function for when the user clicks on a General Category link label************************************************************/

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            previousLink.BackColor = System.Drawing.SystemColors.Control;
            ((LinkLabel)sender).BackColor = System.Drawing.SystemColors.ScrollBar;
            //first we use the previousLink's value, and then we update it
            previousLink = (LinkLabel)sender;


            ((LinkLabel)sender).LinkVisited = true;
            Globals.addHistoryObject(sender, e);
            Globals.addObject(sender, e);
            //historyObject.Add(sender);

            if (e.Button == MouseButtons.Left)
            {
                /*delete previous photoes*/
                picturesFlowPanel.Controls.Clear();

                //gets the name of the LinkLabel that fired the event and after that, the last part of the name which is the id 
                string generalId = ((LinkLabel)sender).Name.Split('-').Last();
                string[] positionArray = ((LinkLabel)sender).Name.Split('-');
                int position = Int32.Parse(positionArray[positionArray.Count() - 2]);

                /*fill in the main text panel with the corresponding text of the specific General Category*/
                GeneralCategory singleGeneralCategory = categoryDAO.getSingleGeneralCategory(generalId);
                /*set the main text and the image*/
                mainLabelText.Text = singleGeneralCategory.text;
                picturesFlowPanel.BackgroundImage = Image.FromFile("..\\..\\Resources\\" + singleGeneralCategory.image);


                //if the switch for the particular general category link is off, iterate through all the innerLinkLabel array and then if the members of the array are the ones we are looking for unhide them.
                if (!genCategorySwitch[position])
                {
                    for (int i = 0; i < innerLinkLabel.Count; i++)
                    {
                        string[] innerLinkLabelNameArray = innerLinkLabel[i].Name.Split('-');
                        string generalCategoryIdFromInnerLinkLabelArray = innerLinkLabelNameArray[innerLinkLabelNameArray.Count() - 2];
                        if (generalId.Equals(generalCategoryIdFromInnerLinkLabelArray))
                        {
                            innerLinkLabel[i].Show();
                        }
                    }
                    genCategorySwitch[position] = true;
                }
                else
                {
                    for (int i = 0; i < innerLinkLabel.Count; i++)
                    {
                        string[] innerLinkLabelNameArray = innerLinkLabel[i].Name.Split('-');
                        string generalCategoryIdFromInnerLinkLabelArray = innerLinkLabelNameArray[innerLinkLabelNameArray.Count() - 2];
                        if (generalId.Equals(generalCategoryIdFromInnerLinkLabelArray))
                        {
                            innerLinkLabel[i].Hide();
                        }
                    }
                    genCategorySwitch[position] = false;
                    
                
                }

            }

        }


        /**********************************************************event handler for the inner link label category**************************************************************/
        private void innerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            previousLink.BackColor = System.Drawing.SystemColors.Control;
            ((LinkLabel)sender).BackColor = System.Drawing.SystemColors.ScrollBar;
            //first we use the previousLink's value, and then we update it
            previousLink = (LinkLabel)sender;

            ((LinkLabel)sender).LinkVisited = true;
            Globals.addHistoryObject(sender, e);
            Globals.addObject(sender, e);
            //historyObject.Add(sender);
            /*clear the initial image*/
            picturesFlowPanel.BackgroundImage = null;
            //string[] innerLinkLabelSplitName = ((LinkLabel)sender).Name.Split('-');
            //the id of the inner category is always one position before the end
            //string categoryStr = innerLinkLabelSplitName[innerLinkLabelSplitName.Length - 2];
            string categoryStr = ((LinkLabel)sender).Name.Split('-').Last();
            int category = Int32.Parse(categoryStr);
            //int category = Int32.Parse(categoryStr);
            /*delete previous photos*/
            picturesFlowPanel.Controls.Clear();
            /*here display photoes*/
            List<Exhibit> exhibitList = dao.displayfromCategory(category);
            DisplayPictures(exhibitList);
          
            //fill in the main text panel
            Category singleCategory = categoryDAO.getSingleCategory(categoryStr);
            mainLabelText.Text = singleCategory.text;
           
        }

        /*a function to display a list of pictures at the "pictures panel" which list is passed as an argument*/
        void DisplayPictures(List<Exhibit> exhibitList)
        {
            foreach (Exhibit exhibitObject in exhibitList)
            {
                /*1. add a new panel*/
                panel.Add(new Panel());
                Panel tempCurrentPanelItem = panel.Last();
                //tempCurrentPanelItem.BackColor = System.Drawing.Color.Gainsboro;
                tempCurrentPanelItem.Size = new System.Drawing.Size(190, 287);

                /*2 add a new pictureBox*/
                pictureBox.Add(new PictureBox());
                PictureBox tempCurrentPictureBox = pictureBox.Last();
                /*in order to access a property from an object in an ArraList, not only casting is neccessary, but also one more
                  parentheses which will make ((Exchibit))o) an exchibit object. Of course, finally we used generics so no need for casting.*/
                tempCurrentPictureBox.BackgroundImage = Image.FromFile("..\\..\\Resources\\" + exhibitObject.image);
                tempCurrentPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                tempCurrentPictureBox.Size = new System.Drawing.Size(150, 205);
                tempCurrentPictureBox.Location = new System.Drawing.Point(20, 29);
                tempCurrentPictureBox.Click += new System.EventHandler(this.MainPanelPictureBox_Click);
                tempCurrentPictureBox.MouseEnter += new System.EventHandler(this.MainPanelPictureBox_MouseEnter);
                tempCurrentPictureBox.MouseLeave += new System.EventHandler(this.MainPanelPictureBox_MouseLeave);
                /*One of the most important lines of code. Shows how we pass data (id and category) by using the name of the control as the "carrier"*/
                tempCurrentPictureBox.Name = "MainPictureBox-" + exhibitObject.category + "-" + exhibitObject.id;

                /*3 add a new linklabel*/
                picturesLinkLabel.Add(new LinkLabel());
                LinkLabel tempCurrentLinkLabelItem = picturesLinkLabel.Last();
                tempCurrentLinkLabelItem.AutoSize = true;
                tempCurrentLinkLabelItem.Location = new System.Drawing.Point(20, 250);
                tempCurrentLinkLabelItem.Size = new System.Drawing.Size(118, 13);
                tempCurrentLinkLabelItem.TabIndex = 1;
                tempCurrentLinkLabelItem.Text = exhibitObject.type;
                tempCurrentLinkLabelItem.Name = "MainPanelLinkLabel-" + exhibitObject.category + "-" + exhibitObject.id;
                tempCurrentLinkLabelItem.Click += new System.EventHandler(this.picturesLinkLabel_Click);
                tempCurrentLinkLabelItem.LinkColor = System.Drawing.Color.Black;

                //add small panel to main panel, the picturebox to small panel and the label to the small panel as well
                picturesFlowPanel.Controls.Add(tempCurrentPanelItem);
                tempCurrentPanelItem.Controls.Add(tempCurrentPictureBox);
                tempCurrentPanelItem.Controls.Add(tempCurrentLinkLabelItem);

            }

        }


        /***********************************************************gets the focus when in picturesFlowPanel, so that the mouse wheel can be used**************************/
        private void picturesFlowPanel_MouseEnter(object sender, EventArgs e)
        {
            if(Form.ActiveForm is MainForm)
                picturesFlowPanel.Focus();
        }

        private void flowLayoutPanel2_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void flowNavigationPanel_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void slideShowPanel_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void slideShowPictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void multimediaPanel_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void calendarPanel_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                rightPanel.Focus();
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                flowLayoutPanel1.Focus();
        }

        private void mainLabelText_MouseEnter(object sender, EventArgs e)
        {
            if (Form.ActiveForm is MainForm)
                flowLayoutPanel1.Focus();
        }

     

        /***********************code regarding slideshow Panel***************************************************************************************************/
        int imageCounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            imageCounter++;
            if (imageCounter < slideShowImageList.Images.Count)
            {
                slideShowPictureBox.BackgroundImage = slideShowImageList.Images[imageCounter];
                slideShowStripLabel.Text = imageCounter + " of " + slideShowImageList.Images.Count + " images.";
            }
            else if (imageCounter == slideShowImageList.Images.Count)
            {
                imageCounter = 0;
                slideShowPictureBox.BackgroundImage = slideShowImageList.Images[imageCounter];
            }
        }

        private void slideShowPlay_Click(object sender, EventArgs e)
        {
            /*execute manually the timer's method so that the image is loaded immediately , then the timer is in charge. This happens because the timer first waits for the interval 
             * time and then executes the code in the timer1_Tick function. This creates a bad feeling to the user, as he receives feedback from the pressed button after the interval time.*/
               
            timer1_Tick(null, null);
            if(!slideShowTimer.Enabled) slideShowTimer.Enabled = true;
        }

        private void slideShowPlay_MouseDown(object sender, MouseEventArgs e)
        {
            slideShowPlay.Image = Properties.Resources.Play_Pressed.ToBitmap();
        }

        private void slideShowStop_Click(object sender, EventArgs e)
        {
            
            if (slideShowTimer.Enabled) slideShowTimer.Enabled = false;

            slideShowPictureBox.BackgroundImage = Properties.Resources.museum;

            slideShowStripLabel.Text  = "0 images of " + slideShowImageList.Images.Count + ".";
            //we finally reset the imageCounter;
            imageCounter = 0;

            //always when stop is pressed, turns the pause to normal mode if it is not already
            if (slideShowPause.Image != Properties.Resources.Pause_Normal.ToBitmap())
            {
                slideShowPause.Image = Properties.Resources.Pause_Normal.ToBitmap();
            }
           
        }

        private void slideShowStop_MouseDown(object sender, MouseEventArgs e)
        {
            slideShowStop.Image = Properties.Resources.Stop_Pressed_Blue.ToBitmap();
        }

        private void slideShowStop_MouseUp(object sender, MouseEventArgs e)
        {
            slideShowStop.Image = Properties.Resources.Stop_Normal_Blue.ToBitmap();
        }

        private void slideShowPlay_MouseUp(object sender, MouseEventArgs e)
        {
            slideShowPlay.Image = Properties.Resources.play_normal1.ToBitmap();
        }

        private void slideShowPause_Click(object sender, EventArgs e)
        {
            
            if (slideShowTimer.Enabled)
            {
                slideShowPause.Image = Properties.Resources.Pause_Pressed.ToBitmap();
                slideShowTimer.Enabled = false;
            }//if we did not define the below statement, then pause would act as play, whenever the timer was not enabled. We need of course pause to act as play but ONLY in the middle of a slide show Section.
            else if(imageCounter!=0)
            {
                slideShowPause.Image = Properties.Resources.Pause_Normal.ToBitmap();
                slideShowTimer.Enabled = true;
            }
        }

        private void slideShowPlay_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void slideShowPlay_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void slideShowStop_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void slideShowStop_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void slideShowTrackBar_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void slideShowTrackBar_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void slideShowPause_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void slideShowPause_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void slideShowTrackBar_ValueChanged(object sender, EventArgs e)
        {
            switch (((TrackBar)sender).Value)
            { 
                case 0:
                    slideShowTimer.Interval = 3000;
                    break;
                case 1:
                    slideShowTimer.Interval = 2500;
                    break;
                case 2:
                    slideShowTimer.Interval = 1500;
                    break;
                case 3:
                    slideShowTimer.Interval = 1000;
                    break;
                case 4:
                    slideShowTimer.Interval = 500;
                    break;
            
            }
        }


        /***********************history button******************************************************************************************************/
        private void historyButton_Click(object sender, EventArgs e)
        {
            if (Globals.historyObjectList.Count > 1)
            {
                /*click events that are stored at the one and only historyObjectList are divided into two categories. The ones that when the back button is pressed, they have to skip the last event
                 and bring on the foreground the prelast and the ones that result in a new window opening, so before applying the functionality to back button, we need to see whether this window is open*/
                //get the last object from the history list
                HistoryObject lastObject = Globals.historyObjectList.Last();

               
                //if we are at the navigation panel, we need to throw the last object and use the prelast
                //we gather 6 different event handlers, so 3 different if statements
                //LinkLabels in Navigation Panels
                if (lastObject.obj is LinkLabel && (((LinkLabel)lastObject.obj).Name.StartsWith("GenCatLinkLabel")  ||  (((LinkLabel)lastObject.obj).Name.StartsWith("innerLinkLabel"))))
                {
                    //remove the last item from the list
                    Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                    //get the now last item from the list and use it to call the corresponding eventHandler function. This is the logic behind the history button. In each click, we "fire" the event handlers, without having events.
                    lastObject = Globals.historyObjectList.Last();
                    
                    /*if the prelast is general category, call its event handler method*/
                    if (lastObject.obj is LinkLabel && (((LinkLabel)lastObject.obj).Name.StartsWith("GenCatLinkLabel")))
                    {
                        linkLabel_LinkClicked(lastObject.obj, (LinkLabelLinkClickedEventArgs)lastObject.e);
                    }
                    /*if the prelast is inner link call its event handler method*/
                    else if (lastObject.obj is LinkLabel && (((LinkLabel)lastObject.obj).Name.StartsWith("innerLinkLabel")))
                    {
                        innerLinkLabel_LinkClicked(lastObject.obj, (LinkLabelLinkClickedEventArgs)lastObject.e);
                    }
                    //remove again the last object which was added in the previous event handling method
                    Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                }



                //right and left arrows in SingleExhibitForm
                else if (lastObject.obj is PictureBox && (((PictureBox)lastObject.obj).Name.StartsWith("arrow"))) //->
                {
                    string[] arrowPictureNameArray = ((PictureBox)lastObject.obj).Name.Split('-');
                    //Because they are both defined to be in the specific places. -1  and +1 because the right sender with id x takes you to the form with id x+1 and the opposite for the left sender
                    int id = Int32.Parse(arrowPictureNameArray[1]);

                    /*if (((PictureBox)lastObject.obj).Name.StartsWith("arrowPictureLeft"))
                    {
                        id = Int32.Parse(arrowPictureNameArray[1]) - 1;
                    }
                        //meaning "starts with 'arrowPictureRight'"
                    else 
                    {
                        id = Int32.Parse(arrowPictureNameArray[1]) + 1;
                    }*/

                    int category = Int32.Parse(arrowPictureNameArray[2]);

                    /*if the last form is open, just bring in in front. if it is not, then open it*/
                    FormCollection fc = Application.OpenForms;
                    foreach (Form f in fc) 
                    {  /*if the id in the name of the form is equal to the id in the name of the arrow, then our form is open, so we will just bring it in front*/
                        if(f.Name.Split('-').Last() == id.ToString())
                        {
                            f.Focus();
                            //remove the last item from the list
                            Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                            return;
                        }
                    }
                    /*if the form is not open, open it. We will not use an event here, because the click events of the arrows lead us to new forms. We will just open a form, using the id and category*/
                    //MainPanelPictureBox_Click(lastObject.obj, lastObject.e);
                    new SingleExhibitForm(id,category,null,null).Show();
                    //remove the last item from the list
                    Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                }



                //picturebox and linklabel in main panel
                else if ((lastObject.obj is PictureBox && ((PictureBox)lastObject.obj).Name.StartsWith("MainPictureBox")) || (lastObject.obj is LinkLabel && ((LinkLabel)lastObject.obj).Name.StartsWith("MainPanelLinkLabel")))
                {
                    string[] mainPanelPictureBoxorLinkLabelNameArray;
                    if (lastObject.obj is PictureBox)
                    {
                        mainPanelPictureBoxorLinkLabelNameArray = ((PictureBox)lastObject.obj).Name.Split('-');
                    }
                    else 
                    {
                        mainPanelPictureBoxorLinkLabelNameArray = ((LinkLabel)lastObject.obj).Name.Split('-');
                    }
                    
                    //Because they are both defined to be in the specific places, 2 and 1 accordingly.
                    int id = Int32.Parse(mainPanelPictureBoxorLinkLabelNameArray[2]);
                    int category = Int32.Parse(mainPanelPictureBoxorLinkLabelNameArray[1]);

                    /*if the last form is open, just bring in in front. if it is not, then open it*/
                    FormCollection fc = Application.OpenForms;
                    foreach (Form f in fc)
                    {  /*if the id in the name of the form is equal to the id in the name of the arrow, then our form is open, so we will just bring it in front*/
                        if (f.Name.Split('-').Last() == id.ToString())
                        {
                            f.Focus();
                            //remove the last item from the list
                            Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                            return;
                        }
                    }
                    /*if the form is not open, open it. We will not use an event here, because the click events of the arrows lead us to new forms. We will just open a form, using the id and category*/
                    //MainPanelPictureBox_Click(lastObject.obj, lastObject.e);
                    new SingleExhibitForm(id, category,null,null).Show();
                    //remove the last item from the list
                    Globals.historyObjectList.RemoveAt(Globals.historyObjectList.Count - 1);
                }
            }    
        }

        private void aboutUsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = Properties.Resources.about_vrettos + " " + Properties.Resources.about_skourtaniotis;
            string caption = Properties.Resources.about_caption;
            MessageBox.Show(message,caption);
            
        }


     

        /*********************************************************multimedia panel**********************************************************/

        /*play button*/
        private void multimediaPlay_MouseDown(object sender, MouseEventArgs e)
        {
            multimediaPlay.Image = Properties.Resources.Play_Pressed.ToBitmap();
        }

        private void multimediaPlay_MouseClick(object sender, MouseEventArgs e)
        {
            if (musicOnOffToolStripMenuItem1.Checked == false)
            {
                player.Play();
                musicOnOffToolStripMenuItem1.Checked = true;
            }
        }

        private void multimediaPlay_MouseUp(object sender, MouseEventArgs e)
        {
            multimediaPlay.Image = Properties.Resources.play_normal1.ToBitmap();
        }

        private void multimediaPlay_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void multimediaPlay_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void multimediaStop_MouseDown(object sender, MouseEventArgs e)
        {
            multimediaStop.Image = Properties.Resources.Stop_Pressed_Blue.ToBitmap();
        }

        private void multimediaStop_MouseClick(object sender, MouseEventArgs e)
        {
            if (musicOnOffToolStripMenuItem1.Checked == true)
            {
                player.Stop();
                musicOnOffToolStripMenuItem1.Checked = false;
            }
        }

        private void multimediaStop_MouseUp(object sender, MouseEventArgs e)
        {
            multimediaStop.Image = Properties.Resources.Stop_Normal_Blue.ToBitmap();
        }

        private void multimediaStop_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void multimediaStop_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
    
        private void musicOnOffToolStripMenuItem1_Click_1(object sender, EventArgs e)
         {
            
             if (musicOnOffToolStripMenuItem1.Checked==false)
             {
                 player.Play();
                 musicOnOffToolStripMenuItem1.Checked = true;
             }
             else
             {
                 player.Stop();
                 musicOnOffToolStripMenuItem1.Checked = false;
             }
         }




       /*********************************************************calendar panel**********************************************************/
        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {   //the user gets to select only one date each time
            if (monthCalendar.BoldedDates.Contains(e.Start))
            {
                int calendarId=-1;

                /*in order to avoid the iteration, we could use the same trick we have used in similar cases. When the control is first inserted, give it a name which includes as part of it
                the data we are interested of and then access the control and strip that data from the name and use them. However, here, we could not do this, as we could not access the specific
                date field in the monthCalendar in first place, to give it a unique name. So iteration it is.*/
                foreach(Calendar cal in calendarList)
                {   /*if the date that the user selected is the Same with the date passed in calendar object take the id and send it to the MuseumCalendarEventForm*/
                    if (e.Start == cal.date)
                    {
                        calendarId = cal.id;
                        break;
                    }
                }
                new MuseumCalendarEventForm(calendarId).Show();
            }
        }

        /*****************************************Login/logout/signup functionality******************************************************/
        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login loginForm = Login.getInstance();
            loginForm.Show();
            loginForm.LoggedIn += new Login.OnLoggedInEventHandler(ShowUserName);
        }

        public void ShowUserName(object sender, EventArgs e)
        {
            userToolBoxcomboBox.Text = "Hello, " + Globals.currentUser.username;
            userToolBoxcomboBox.Items.Add(Properties.Resources.logout);

            /*we disable and enable the corresponding menu items*/
            logInToolStripMenuItem.Enabled = false;
            createAProfileToolStripMenuItem.Enabled = false;
            editProfileToolStripMenuItem1.Enabled = true;
            logOutToolStripMenuItem1.Enabled = true;
        }

        /*Log out can take place from two spots*/
        private void userToolBoxcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.currentUser = null;
            userToolBoxcomboBox.Items.Remove(Properties.Resources.logout);
            MessageBox.Show("Successful logout!");
            logInToolStripMenuItem.Enabled = true;
            createAProfileToolStripMenuItem.Enabled = true;
            editProfileToolStripMenuItem1.Enabled = false;
            logOutToolStripMenuItem1.Enabled = false;
            userToolBoxcomboBox.Text = "No Logged in user"; 
        }

        private void logOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Globals.currentUser = null;
            userToolBoxcomboBox.Items.Remove(Properties.Resources.logout);
            MessageBox.Show("Successful logout!");
            logInToolStripMenuItem.Enabled = true;
            createAProfileToolStripMenuItem.Enabled = true;
            editProfileToolStripMenuItem1.Enabled = false;
            logOutToolStripMenuItem1.Enabled = false;
            userToolBoxcomboBox.Text = "No Logged in user";            
        }


        //Log in and edit options have to be enabled in order the user to access them
        private void createAProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createAProfileToolStripMenuItem.Enabled)
            {
                SignUpForm signUpForm = SignUpForm.getSignUpInstance(0);
                signUpForm.Show();
                signUpForm.UserSignedUp += new SignUpForm.UserSignedUpEventHandler(ShowUserName);
            }
        }

        private void editProfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (editProfileToolStripMenuItem1.Enabled)
            {
                SignUpForm editSignUpForm = SignUpForm.getSignUpInstance(Globals.currentUser.id);
                editSignUpForm.Show();
            
            }
        }

        /*********************************************************Exits the application**********************************************************/
        private void exitApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /*********************************************************Visitor book*******************************************************************/
        private void visitorsBook_Click(object sender, EventArgs e)
        {
            if (Globals.currentUser == null)
            {
                MessageBox.Show("This area is only accessed by Logged in users.");
            }
            else
            {
                VisitorBook.getVisitorBookInstance().Show();
            }
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (Globals.currentUser == null)
            {
                MessageBox.Show("This area is only accessed by Logged in users.");
            }
            else
            {
                VisitorBook.getVisitorBookInstance().Show();
            }
            
        }

        /*********************************************************Visit session*******************************************************************/

        private void recordAVisitSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {

           // if the record button has the visitsession gif, smthing that indicates that it is playing a session, then deactivate it. we did not use the enable property for this because it deactivated the animation
            if (recordbtn.Tag ==null)
            {
                if (Globals.currentUser == null)
                {
                    MessageBox.Show("You need to sign up or log in in order to access this functionality.");
                }
                else
                {
                    Globals.sessionStarted = true;
                    //show the record image pressed
                    recordbtn.Image = Properties.Resources.Record_Pressed_icon;
                    recordLabel.Text = "Recording...";
                    recordLabel.ForeColor = Color.Red;
                }
            }
        }
        //the record button in the toolStrip detects whether the record session has started and acts accordingly by calling the eventhandlers of the corresponding menu items
        private void recordbtn_Click(object sender, EventArgs e)
        {
            if (recordbtn.Tag == null)
            {
                if (!Globals.sessionStarted)
                {
                    recordAVisitSessionToolStripMenuItem_Click(null, null);
                }
                else
                {
                    stopVisitSessionRecordingToolStripMenuItem_Click(null, null);
                }
            }
        }


        private void recordbtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (recordbtn.Tag == null)
            {
                if (Globals.currentUser == null)
                {
                    ((ToolStripButton)sender).Image = Properties.Resources.Record_Normal_icon;
                }
            }
        }

        private void recordbtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (recordbtn.Tag == null)
            {
                ((ToolStripButton)sender).Image = Properties.Resources.Record_Pressed_icon;
            }
        }


        private void stopVisitSessionRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.sessionStarted == true)
            {

                if (Globals.sessionObjectList.Count() == 0)
                {
                    MessageBox.Show("You did not visit any exhibit. Please try again.");
                }
                else
                {
                    //we borrow the input messagebox from visualbasic. Quick and dirty solution in cases like this, when we do not want to build a new class.
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Visit Session Name", "Please write a name for your visit session.", "Name", 0);

                    //If the user clicks Cancel, a zero-length string is returned
                    if (input == "")
                    {
                        MessageBox.Show("The visit session was not recorded");
                        //reseting
                        Globals.sessionStarted = false;
                        Globals.sessionStarted = false;
                        recordbtn.Image = Properties.Resources.Record_Normal_icon;
                        recordLabel.Text = "Record";
                        recordLabel.ForeColor = Color.Black;
                        Globals.sessionObjectList.Clear();
                    }
                    else
                    {
                        /*sets the global static variable started equal to false, so as to stop recording and keeps the list in the database*/
                        Globals.sessionStarted = false;
                        SessionListObjectDAO sessionListObjectDAO = new SessionListObjectDAO();

                        SessionListObject slo = new SessionListObject();
                        slo.userId = Globals.currentUser.id;
                        slo.sessionList = Globals.sessionObjectList;
                        slo.sessionName = input;
                        sessionListObjectDAO.InsertSessionListObject(slo);
                        //reset the flag
                        Globals.sessionStarted = false;
                        //reset the toolstrip button
                        recordbtn.Image = Properties.Resources.Record_Normal_icon;
                        recordLabel.Text = "Record";
                        recordLabel.ForeColor = Color.Black;
                        //resset the sessionObjectList
                        Globals.sessionObjectList.Clear();
                    }
                }
            }
        }


        private void loadCurrentVisitSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.currentUser != null)
            {
                var sessionOptions = new SessionOptions();
                sessionOptions.Show();
                //we have here a reference to an instance of the SessionOptions class, so subscribe the eventhandler function to the event SessionChosen
                sessionOptions.SessionChosen += SessionOptions_SessionChosen;
            }
        }

        private void resetCurrentVisitSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.sessionStarted == true)
            {
                Globals.sessionObjectList.Clear();
                MessageBox.Show("You can now start the visit session from the beginning.");
            }
        }


        private void SessionOptions_SessionChosen(object sender, SessionEventArgs args)
        {
            //we are passing the args data to the tag property of the timer
            visitSessionTimer.Tag = args;

            //setting the record btn and label at the working ribbon at the appropriate states
            recordLabel.Text = "Executing visit session \"" + ((SessionEventArgs)visitSessionTimer.Tag).sessionListObject.sessionName + "\"";
            recordLabel.ForeColor = Color.Red;
            recordbtn.Image = Properties.Resources.visitsession;
            recordbtn.Tag = "someTag";

            visitSessionTimer.Enabled = true;
            visitSessionTimer_Tick(sender, args);
        }

        //the Tick method acts as a loop so there isn't a need for a foreach loop (an iterator). Just access one element of the sessionList each time
        int counterOfSessionList = 0;
        private void visitSessionTimer_Tick(object sender, EventArgs e)
        {
            

            //check that ArrayIndexOutOfBoundsException will not be thrown
            if (counterOfSessionList < ((SessionEventArgs)visitSessionTimer.Tag).sessionListObject.sessionList.Count()) {
                

                //get the element of the sessionList
                SessionObject slo = ((SessionEventArgs)visitSessionTimer.Tag).sessionListObject.sessionList[counterOfSessionList];
                Type elementType = ((SessionEventArgs)visitSessionTimer.Tag).sessionListObject.sessionList[counterOfSessionList].type;
                string elementName = ((SessionEventArgs)visitSessionTimer.Tag).sessionListObject.sessionList[counterOfSessionList].Name;

                

                //case General Category LinkLabel
                if (elementType == typeof(LinkLabel) && elementName.StartsWith("GenCatLinkLabel"))
                {
                    //we create two temporary "dummy" objects in order to pass them to the appropriate eventhandler. We pass to them only the data we care about
                    LinkLabel tempLinkLabel = new LinkLabel();
                    tempLinkLabel.Name = elementName;

                    LinkLabelLinkClickedEventArgs args = new LinkLabelLinkClickedEventArgs(null,MouseButtons.Left);

                    linkLabel_LinkClicked(tempLinkLabel, args);

                }
                //case simple category Link Label
                else if (elementType == typeof(LinkLabel) && elementName.StartsWith("innerLinkLabel"))
                {
                    LinkLabel tempLinkLabel = new LinkLabel();
                    tempLinkLabel.Name = elementName;
                    LinkLabelLinkClickedEventArgs args = new LinkLabelLinkClickedEventArgs(null, MouseButtons.Left);

                    innerLinkLabel_LinkClicked(tempLinkLabel, args);

                }//case picture Link Label
                else if (elementType == typeof(LinkLabel) && elementName.StartsWith("MainPanelLinkLabel"))
                {
                    LinkLabel tempLinkLabel = new LinkLabel();
                    tempLinkLabel.Name = elementName;

                    singleFormAppear(tempLinkLabel);
                }//case picturebox
                else if (elementType == typeof(PictureBox) && elementName.StartsWith("MainPictureBox"))
                {
                    PictureBox tempPictureBox = new PictureBox();
                    tempPictureBox.Name = elementName;

                    singleFormAppear(tempPictureBox);
                }
                //case picturebox - left arrow
                else if (elementType == typeof(PictureBox) && elementName.StartsWith("arrowPictureLeft"))
                {
                    string[] elementNameArray = elementName.Split('-');
                    int id = Int32.Parse(elementNameArray[1]);
                    int category = Int32.Parse(elementNameArray[2]);

                    new SingleExhibitForm(id, category,null,null).Show();

                }//case picturebox - right arrow
                else if (elementType == typeof(PictureBox) && elementName.StartsWith("arrowPictureRight"))
                {
                    string[] elementNameArray = elementName.Split('-');
                    int id = Int32.Parse(elementNameArray[1]);
                    int category = Int32.Parse(elementNameArray[2]);

                    new SingleExhibitForm(id, category,null,null).Show();
                }
                counterOfSessionList++;
            }
            else
            {
                counterOfSessionList = 0;
                visitSessionTimer.Enabled = false;
                recordLabel.Text = "Record";
                recordLabel.ForeColor = Color.Black;
                recordbtn.Image = Properties.Resources.Record_Normal_icon;
                recordbtn.Tag = null;
            }
            
        }


        /******************************************************************************************************************************************************/


        private void clearHistory_Click(object sender, EventArgs e)
        {
            /*clear the history list, but keep the last element*/
            var lastObjectInList = Globals.historyObjectList.Last();
            Globals.historyObjectList.Clear();
            Globals.historyObjectList.Add(lastObjectInList);


            GetAllControls(flowNavigationPanel.Controls);
            GetAllControls(picturesFlowPanel.Controls);
        }


        public void GetAllControls(IList ctrls)
        {
           
            foreach (Control ctl in ctrls)
            {
                if (ctl is LinkLabel) 
                {
                    ((LinkLabel)ctl).LinkVisited = false;
                    
                }
                GetAllControls(ctl.Controls);
            }
         
        }

        

       
       
    }
}
