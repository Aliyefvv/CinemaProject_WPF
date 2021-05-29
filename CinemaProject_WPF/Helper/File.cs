using CinemaProject_WPF.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Helper
{
    public static class File
    {
        public static void UpdateUser(List<User> users)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter("users.json",false))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(jw, users);
            }
        }

        public static void WriteJSON(object obj, string filename)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(filename))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(jw, obj);
            }
        }

        public static List<User> ReadJSON_Users(string filename)
        {
            var serializer = new JsonSerializer();
            var fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (var sr = new StreamReader(fs))
            using (var jr = new JsonTextReader(sr))
                return serializer.Deserialize<List<User>>(jr);
        }

        public static List<Ticket> ReadJSON_Tickets(string filename)
        {
            var serializer = new JsonSerializer();
            var fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (var sr = new StreamReader(fs))
            using (var jr = new JsonTextReader(sr))
                return serializer.Deserialize<List<Ticket>>(jr);
        }

        public static void PrintVaucherPDF(Ticket ticket)
        {
            Guid ID = Guid.NewGuid();
            iTextSharp.text.Document oDoc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(oDoc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                 + "\\\\" + ID.ToString() + ".pdf", FileMode.Create));
            oDoc.Open();
            iTextSharp.text.Image companyLogo = iTextSharp.text.Image.GetInstance("../../Assets/BuyTicketPage/Images/cinemaplus.png");
            companyLogo.Alignment = Element.ALIGN_CENTER;
            companyLogo.ScaleAbsolute(250, 75);
            oDoc.Add(companyLogo);
            oDoc.Add(new Paragraph($"                                     ======================================="));
            oDoc.Add(new Paragraph($"                                       Date : {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}"));
            oDoc.Add(new Paragraph($"                                     ======================================="));
            oDoc.Add(new Paragraph($"                                       Movie Name : {ticket?.MovieName}"));
            oDoc.Add(new Paragraph($"                                       Date : {ticket?.Date.ToShortDateString()}"));
            oDoc.Add(new Paragraph($"                                       Time : {ticket?.Time}"));
            oDoc.Add(new Paragraph($"                                       Ticket Count : {ticket?.TicketCount}"));
            string nums = "";
            foreach (var number in ticket?.TicketNumbers)
                nums += number.ToString() + ", ";
            oDoc.Add(new Paragraph($"                                       Ticket Numbers : {nums}"));
            oDoc.Add(new Paragraph($"                                     -------------------------------------------------------------------"));
            bool haveProduct = false;
            foreach (var item in ticket.Snacks)
                if (item.Value > 0) { oDoc.Add(new Paragraph($"                                       {item.Key} : {item.Value}")); haveProduct = true; }
            if(haveProduct) oDoc.Add(new Paragraph($"                                     -------------------------------------------------------------------"));
            oDoc.Add(new Paragraph($"                                       TOTAL : {ticket?.Total} azn"));
            oDoc.Add(new Paragraph($"                                     ======================================="));
            oDoc.Close();
        }
    }
}
