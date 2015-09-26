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
    public partial class MuseumCalendarEventForm : Form
    {

        CalendarDAO calendarDao = new CalendarDAO();
        Calendar calendar = new Calendar();


        /*we are passing the id to the constructor*/
        public MuseumCalendarEventForm(int id)
        {
            InitializeComponent();

            calendar = calendarDao.getSingleCalendarData(id);

            eventFormpictureBox.BackgroundImage = Image.FromFile("..//..//Resources//" + calendar.image);
            eventFormTitle.Text = calendar.eventTitle;
            eventFormDate.Text = calendar.date.ToString();
            eventFormText.Text = calendar.text;
        }
    }
}
