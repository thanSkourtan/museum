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
    public partial class SingleExhibitForm : Form
    {
        public int id { set; get; }
        public int category { set; get; }

        public string name { get; set; }
        ExhibitDAO dao;
        Exhibit mainExhibit;
           

        public SingleExhibitForm(int id,int category, object sender,EventArgs e)
        {
            InitializeComponent();

            this.id=id;
            this.category = category;
            dao = new ExhibitDAO();
            mainExhibit = dao.displayFromId(id);
            
            

            //Image.Fromfile returns an Image object
            singlePictureBox.BackgroundImage = Image.FromFile("..\\..\\Resources\\" + mainExhibit.image);
            singlePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            singleLabelPlace.Text = mainExhibit.place;
            singleLabeltype.Text = mainExhibit.type;
            singleLabelDimensions.Text = mainExhibit.dimensions;
            singleLabelDescription.Text = mainExhibit.description;
            singleLabelTiming.Text = mainExhibit.timing;
            singleLabelArea.Text = mainExhibit.area;
            singleLabelDescription.MaximumSize = new Size(296, 0);
            singleLabelDescription.AutoSize = true;
            /*here we pass data that we are going to need for the history functionality of the application to the name of the sender object. So when we would get the sender in the 
              historyObjectList, we will have these data available. For the same reason, each SingleExhibitForm object takes a different name.*/
            arrowLeftPicture.Name = "arrowPictureLeft-" + id + "-" + category;
            arrowRightPictured.Name = "arrowPictureRight-" + id + "-" + category;

            /*unique name*/
            this.Name = "form-" + id;

            arrowLeftPicture.BackgroundImage = Properties.Resources.arrow_alt_left;
            arrowRightPictured.BackgroundImage = Properties.Resources.arrow_alt_right;


            //don't add a history or a session object if the creation of the form was not a result of an event handler. In such case the event handler that created the form will add it
            if (sender != null && ((PictureBox)sender).Name.StartsWith("arrowPictureLeft"))
            {
                Globals.addHistoryObject(arrowLeftPicture, e);
                Globals.addObject(arrowLeftPicture, e);
            }else if(sender != null && ((PictureBox)sender).Name.StartsWith("arrowPictureRight"))
            {
                Globals.addHistoryObject(arrowRightPictured, e);
                Globals.addObject(arrowRightPictured, e);
            }


        }

        //left button
        /*the click events need to be declared public in order to be accessed by the MainForm class*/
        public void pictureBox2_Click(object sender, EventArgs e)
        {
            
            bool found = false;

            List<Exhibit> exhibitList = dao.displayfromCategory(category);
            //if we are at the first position of the list do nothing
            if (exhibitList.First().id != mainExhibit.id)
            {
                /*using a for loop instead of a foreach loop in order to iterate the list in reverse order*/
                for (int i = exhibitList.Count() - 1; i >= 0; i--)
                {

                    if (found == true)
                    {
                        var nextForm = new SingleExhibitForm(exhibitList[i].id, exhibitList[i].category,sender,e);
                        nextForm.singlePictureBox.BackgroundImage = Image.FromFile("..\\..\\Resources\\" + exhibitList[i].image);
                        nextForm.singlePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        nextForm.singleLabelPlace.Text = exhibitList[i].place;
                        nextForm.singleLabeltype.Text = exhibitList[i].type;
                        nextForm.singleLabelDimensions.Text = exhibitList[i].dimensions;
                        nextForm.singleLabelDescription.Text = exhibitList[i].description;
                        nextForm.singleLabelTiming.Text = exhibitList[i].timing;
                        nextForm.singleLabelArea.Text = exhibitList[i].area;
                        nextForm.singleLabelDescription.MaximumSize = new Size(296, 0);
                        nextForm.singleLabelDescription.AutoSize = true;
                        nextForm.Show();

                        //closes the current form
                        this.Close();
                        /*the form is closed but not destroyed so we keep on*/
                        break;
                    }
                    /*if you find the id change the flag and show the next exhibit on the following iteration. if we are at the last exhibit then nothing happens, which is something expected*/
                    if (exhibitList[i].id == mainExhibit.id)
                    {
                        //we add the historyObject ONLY IF the exhibit is found and displayed
                        //Globals.addHistoryObject(sender, e);
                        found = true;
                    }
                }
            }
        }

        //right button
        public void pictureBox1_Click(object sender, EventArgs e)
        {
            
                bool found = false;

                List<Exhibit> exhibitList = dao.displayfromCategory(category);
                //if we are at the last position of the list do nothing
                if(exhibitList.Last().id!= mainExhibit.id)
                {
                    foreach (Exhibit ex in exhibitList)
                    {
                        if (found == true)
                        {
                            var nextForm = new SingleExhibitForm(ex.id,ex.category,sender,e);
                            nextForm.singlePictureBox.BackgroundImage = Image.FromFile("..\\..\\Resources\\" + ex.image);
                            nextForm.singlePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            nextForm.singleLabeltype.Text = ex.type;
                            nextForm.singleLabelDimensions.Text = ex.dimensions;
                            nextForm.singleLabelDescription.Text = ex.description;
                            nextForm.singleLabelTiming.Text = ex.timing;
                            nextForm.singleLabelArea.Text = ex.area;
                            nextForm.singleLabelDescription.MaximumSize = new Size(296, 0);
                            nextForm.singleLabelDescription.AutoSize = true;
                            nextForm.Show();
                            //closes the current form
                            this.Close();
                            /*the form is closed but not destroyed so we keep on*/
                            break;
                        }
                        /*if you find the id change the flag and show the next exhibit on the following iteration. if we are at the last exhibit then nothing happens, which is something expected*/
                        if (ex.id == mainExhibit.id )
                        {   
                            
                            found = true;
                        }
                    }
            }
        }

        private void arrowLeftPicture_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void arrowLeftPicture_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void arrowRightPictured_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void arrowRightPictured_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
    }
}
