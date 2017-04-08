using Microsoft.AspNetCore.Mvc;
using Angular2.Models;
using System;
using System.IO;
using System.Collections;
using ImageMagick;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller

{
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }

    public IActionResult notIndex() 
    {
        
        Image image = new Image();
        image = processExif(@"C:\Photo\20170325_170019.JPG");
        
        return View(image);

    }
    
    public ArrayList getFilesNames() {

        ArrayList paths = new ArrayList();
        
        string source = @"C:\Photo";

        try 
        {
            var jpgFiles = Directory.EnumerateFiles(source, "*.jpg");

            foreach (string file in jpgFiles) {
                paths.Add(file);
            }

        }
        catch (Exception e) {
            Console.WriteLine(e);
        }

        return paths;
    }

    public Image processExif(string path) {
        
        Image image = new Image();

        using (MagickImage exifImage = new MagickImage(path))
        {
            // Retrieve the exif information
            
            ExifProfile profile = exifImage.GetExifProfile();

            // Check if image contains an exif profile
            if (profile == null)
                image.info = "Image does not contain exif information.";
            else
            {   
                foreach (ExifValue value in profile.Values)
                {
                    Console.WriteLine(value.Tag.ToString() + ": " + value.ToString());
                    setAllValues(value, image);
                }
            }
        }

        return image;
    }

    public void setAllValues(ExifValue value, Image image) {

            string hold = value.ToString();

            if (value.Tag.ToString().Equals("GPSLatitudeRef")) {
                
                image.GPSLatitudeRef = hold[0];

            } else if (value.Tag.ToString().Equals("GPSLatitude")) {

                image.GPSLatitude = hold;
                image.LatitudeDegree = parseDegree(hold);
                image.LatitudeMinute = parseMinute(hold);
                image.LatitudeSecond = parseSecond(hold);

            } else if (value.Tag.ToString().Equals("GPSLongitudeRef")) {

                image.GPSLongitudeRef = hold[0];

            } else if (value.Tag.ToString().Equals("GPSLongitude")) {

                image.GPSLongitude = hold;
                image.LongitudeDegree = parseDegree(hold);
                image.LongitudeMinute = parseMinute(hold);
                image.LongitudeSecond = parseSecond(hold);

            } else if (value.Tag.ToString().Equals("DateTime")) {
                
                image.dateTime = convertDateTime(hold);
                
            }
        }

        public int parseDegree(string value) {

            Boolean run = true;
            int a = 0;
            int b = 0;

            while (run) {
                if (value[b].ToString() == " ") {
                    string sub = value.Substring(a, b);
                    a = Convert.ToInt32(sub);
                    run = false;
                }
                b = b + 1;
            }

            return a;
        }

        public int parseMinute(string value) {

            Boolean run = true;
            Boolean next = false;
            int a = 0;
            int b = 0;

            while (run) {
                if (next) {
                    if (value[b].ToString() == " ") {
                        string sub = value.Substring(a, b - a);
                        a = Convert.ToInt32(sub);
                        run = false;
                    }
                } else {
                    if (value[b].ToString() == " ") {
                        a = b + 1;
                        next = true;
                    }
                }
                b = b + 1;
            }

            return a;
        }

        public double parseSecond(string value) {

            Boolean run = true;
            Boolean next = false;
            double num = 0;
            double den = 0;
            int a = 0;
            int b = 0;
            int length = value.Length;
            string sub = "";

            while (run) {
                if (next) {
                    if (sub.Length == 2 || sub.Length == 3) {
                        num = Convert.ToDouble(sub);                            
                        run = false;
                    } else if (sub[b].ToString() == "/") {
                        num = Convert.ToDouble(sub.Substring(a, b));
                        den = Convert.ToDouble(sub.Substring(b + 1, sub.Length - (b + 1)));
                        num = num / den;
                        run = false;
                    }
                } else {
                    if (value[b].ToString() == " ") {
                        a++;
                        if (a == 2) {
                            next = true;
                            sub = value.Substring(b, length - b);
                            b = 0;
                            a = 0;
                        }
                    }
                }
                b++;
            }

            return num;
        }

        public DateTime convertDateTime(string value) {

            int hour = Convert.ToInt32(value.Substring(11, 2));
            string amPm = "AM";

            if (hour > 12) {
                if (hour == 24) {
                    amPm = "AM";
                } else {
                    amPm = "PM";
                }
                hour = hour - 12;
            } else {
                if (hour == 12) {
                    amPm = "PM";
                }
            }

            string hold = value.Substring(5, 2) + "/" 
                        + value.Substring(8, 2) + "/"
                        + value.Substring(0,4) + " "
                        + hour.ToString() + ":"
                        + value.Substring(14, 2) + ":"
                        + value.Substring(17, 2) + " "
                        + amPm;

            return Convert.ToDateTime(hold);
        }

}
