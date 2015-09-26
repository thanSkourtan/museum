using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace museum
{
    public static class Globals
    {
         public static User currentUser {get; set;}

        /*************all the below are about sessions**************************/
         //public static SessionObject sessionObject = new SessionObject();
         public static bool sessionStarted = false;
         
         public static List<SessionObject> sessionObjectList = new List<SessionObject>();
         
         public static void addObject(Object obj, EventArgs e)
         {   //if the user has started recording, create a new sessionObject, attach the required data on it and add it in the list
             if (sessionStarted)
             {
                 
                 SessionObject sessionObject = new SessionObject();
                 sessionObject.type = obj.GetType();
                 sessionObject.Name = ((Control)obj).Name;
                 sessionObject.button = MouseButtons.Left;
                 //sessionObject.e = e;
                 sessionObjectList.Add(sessionObject);
             }
         }

        /*************all the below are about history**************************/
         public static HistoryObject historyObject { get; set; }

         public static List<HistoryObject> historyObjectList = new List<HistoryObject>();

         public static void addHistoryObject(Object obj, EventArgs e)
         {
             HistoryObject historyObject = new HistoryObject();
             historyObject.obj = obj;
             historyObject.e = e;
             historyObjectList.Add(historyObject);
         
         }

    }
}
