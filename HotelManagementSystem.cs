using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
class Users
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string address { get; set; }

    public string phoneNumber { get; set; }
    public string Email { get; set; }
}
class Guest : Users 
{

    public List<room> UserBookedRooms { get; set; }
    public Guest(string Na, string password, string ad, string PN, string email) 
    {
        Name = Na;
        Password =  password;
        address = ad;
        phoneNumber = PN;
        Email = email;
        UserBookedRooms  = new List<room>();
    }
}
class Receptionist : Users
{

    public Receptionist(string Na, string password)
    {
        Name = Na;
        Password = password;

    }
}
enum RoomType
{ 
    QueenSize = 1,
    TwinBed = 2,
    KingRoom = 3,


}
class room
{
    public int RoomID { get; set; }
    public string HotelName { get; set; }
    public string RoomType { get; set; }

    public int price { get; set; }

    public Boolean available { get; set; }

    public string strt { get; set; }
    public int duration { get; set; }

    public string ReservationNumber {  get; set; }
    public room(string Type, string Hotelname, int RoomId) 
    {
        RoomType = Type;
        HotelName = Hotelname;
        RoomID = RoomId;
        available = true;
        strt = null;
        ReservationNumber = null;
        duration = 0;
        switch (Type) 
        {
            case "QueenSized":
                price = 2500 * duration;
                break;
            case "TwinBed": 
                price = 1500 * duration;
                break;

            case "KingRoom":
                price = 1000 * duration;
                break;
        }
    }
}
class Hotel
{
    public string Name;
    public string loc;
    public List<room> RoomList { get; set; }
    public Hotel(string name,string location) 
    {
        Name = name;
        loc = location;
        RoomList = new List<room>();
        //Since There was no need to have an option to add rooms, We will be adding default rooms per hotel.
        for (int x = 0; x < 9; x++) 
        {
            string type = Enum.GetName(typeof(RoomType), x);
            Random rnd = new Random();
            int num = rnd.Next(1,3);
            room a = new room(type, name, x);
            RoomList.Add(a);
        }
    }
    
}

class Archive
{
    public List<Guest> GuestList { get; set; }
    public List<Hotel> HotelList { get; set; }
    public List<Receptionist> receptsList { get; set; }
    public Archive() 
    {
        GuestList = new List<Guest>();
        receptsList = new List<Receptionist>();
        HotelList = new List<Hotel>();
    }
}

class HSystem
{
    static void Main(string[] args)
    {
        Boolean loop = true;
        Archive arch = new Archive();
        Hotel abss = new Hotel("Amongus", "AFrican wild life preserve");
        arch.HotelList.Add(abss);
        while (loop == true)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Options");
            Console.WriteLine("1. Register membership, \n 2. Display Available rooms, \n 3. Display your reservations(Needs membership), \n 4.)Book(Needs account) \n 5.)Display Via Reservation number \n 6.) Exit");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Name?");
                    string Name = Console.ReadLine();
                    Console.WriteLine("password?");
                    string password = Console.ReadLine();
                    Console.WriteLine("Address?");
                    string address = Console.ReadLine();
                    Console.WriteLine("Phone Number?");
                    string PhoneNumber = Console.ReadLine();
                    Console.WriteLine("Email?");
                    string Email = Console.ReadLine();
                    Register(Name, password, address, PhoneNumber, Email);
                    break;

                case 2:
                    if (arch.HotelList.Count == 0)
                    {
                        Console.WriteLine("Sorry, No hotels have been added yet.");
                        return;
                    }
                    else 
                    {
                        foreach (Hotel x in arch.HotelList)
                        {
                            foreach (room ab in x.RoomList) 
                            {
                                if (ab.available == true)
                                {
                                    Console.Write("\nHotel:\n");
                                    Console.Write(x.Name);
                                    Console.Write("\nAvailable RoomID: \n");
                                    Console.Write(ab.RoomID);
                                }
                                else 
                                {
                                    return;
                                }
                                
                                
                            }
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Please enter the name of the guest.");
                    string GName = Console.ReadLine();
                    Boolean found = false;
                    foreach (Guest guest in arch.GuestList) 
                    {

                        if (guest.Name == GName)
                        {
                            DisplayReservations(guest);
                            found = true;
                        }
                        else 
                        {
                            return;
                        }
                    }
                    if (found == false) 
                    {
                        Console.WriteLine("Sorry, User does not exist");
                    }
                    break;
                case 4:
                    Console.WriteLine("Please enter your name");
                    string Gname = Console.ReadLine();
                    foreach (Guest guest in arch.GuestList) 
                    {
                        if (guest.Name == Gname)
                        {
                            for (int acs = 0; acs < 3; acs++)
                            {
                                Console.WriteLine("Please enter your password");
                                string passw = Console.ReadLine();
                                if (passw == guest.Password)
                                {
                                    Console.WriteLine("Correct! Please proceed to booking.");
                                    foreach (Hotel hotesl in arch.HotelList)
                                    {
                                        Console.WriteLine(hotesl.Name);
                                    }
                                    Console.WriteLine("What hotel would you like?");
                                    string hotel = Console.ReadLine();


                                    foreach (Hotel x in arch.HotelList)
                                    {
                                        if (x.Name == hotel)
                                        {
                                            foreach (room a in x.RoomList)
                                            {
                                                Console.WriteLine("-----RoomID-----");
                                                Console.WriteLine(a.RoomID);
                                                Console.WriteLine("-----Type-----");
                                                Console.WriteLine(a.RoomType);
                                            }
                                            Console.WriteLine("Room would you like?");
                                            int room = int.Parse(Console.ReadLine());
                                            room abert = null;
                                            foreach (room abe in x.RoomList)
                                            {
                                                if (abe.RoomID == room)
                                                {
                                                    abert = abe;
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                            }
                                            Console.WriteLine("When would you like to start?(Please use exact date)");
                                            string startDate = Console.ReadLine();
                                            Console.WriteLine("For how long are you going to stay?(In Days)");
                                            int duration = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Thank you! Preparing to book.");
                                            Book(x, abert, guest, startDate, duration);

                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Hotel");
                                        }

                                    }

                                }
                            }


                        }
                        else
                        {
                            return;
                        }
                    }
                    foreach (Receptionist x in arch.receptsList) 
                    {
                        if (x.Name == Gname) 
                        {
                            Console.WriteLine("What guest would you like to book for?");
                            string nam = Console.ReadLine();
                            foreach (Guest gues in arch.GuestList) 
                            {
                                if (gues.Name == nam)
                                {
                                    foreach (Hotel h in arch.HotelList) 
                                    {
                                        Console.WriteLine(h.Name);
                                    }
                                    Console.WriteLine("What would hotel would you like?");
                                    string nam3 = Console.ReadLine();
                                    foreach (Hotel h in arch.HotelList)
                                    {
                                        if (nam3 == h.Name)
                                        {
                                            room rooma = null;
                                            Console.WriteLine("What will be the room?");
                                            int Roomy = int.Parse(Console.ReadLine());
                                            foreach (room xart in h.RoomList) 
                                            {
                                                if (Roomy == xart.RoomID) 
                                                {
                                                    rooma = xart;
                                                }
                                            }
                                            Console.WriteLine("What is the start date?");
                                           string Strt = Console.ReadLine();
                                            Console.WriteLine("What is the druation in days");
                                            int days = int.Parse(Console.ReadLine());
                                            Book(h, rooma, gues, Strt, days);
                                        }
                                        else 
                                        {
                                            Console.WriteLine("Hotel not found");
                                            return;
                                        }
                                    }
                                }
                            }

                        }

                    }
                    break;
                case 5:
                    Console.WriteLine("Enter User");
                    string name = Console.ReadLine();
                    foreach (Guest guest in arch.GuestList) 
                    {
                        if (guest.Name == name) 
                        {
                            Console.WriteLine("Now please enter the refference number");
                            int refN = int.Parse(Console.ReadLine());
                            DisplayReservationsViaNumber(guest,refN);
                        }
                    }
                    break;
                case 6:
                    loop = false;
                    break;
            }

        }
        void Register(string Na, string password, string ad, string PN, string email)
        {
            Guest a = new Guest(Na, password, ad, PN, email);
            arch.GuestList.Add(a);
        }
        void Book(Hotel Hotel, room Room, Guest guest, string startDate, int Duration) 
        {
            Room.available = false;
            guest.UserBookedRooms.Add(Room);
            Room.strt = startDate;
            Room.duration = Duration;
            int inde = (arch.HotelList.IndexOf(Hotel) + 1 * 8) + arch.GuestList.IndexOf(guest) * guest.UserBookedRooms.IndexOf(Room);
            Console.WriteLine("reservationNumber");
            Console.WriteLine(inde);

        }
        void DisplayReservations(Guest guest) 
        {
            if (guest.UserBookedRooms.Count == 0) { Console.WriteLine("Sorry No reservations yet"); }
            foreach (room x in guest.UserBookedRooms)
            {
                Console.WriteLine("Reservation Room Number:");
                Console.Write(x.RoomID);
                Console.Write(" ,Hotel:");
                Console.Write(x.HotelName);
            }
            

        }
        void DisplayReservationsViaNumber(Guest guest,int ReservationNo)
        {
            foreach (room x in guest.UserBookedRooms)
            {
                if (int.Parse(x.ReservationNumber) == ReservationNo) 
                {
                    Console.WriteLine("____Reservation___");
                    Console.WriteLine($"ReservationID {ReservationNo}, Hotel: {x.HotelName}, RoomID: {x.RoomID}");
                }
  
            }


        }
    }
}
