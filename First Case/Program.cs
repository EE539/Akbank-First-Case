using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

class Program
{

    public static void Main(string[] args)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string read;
        StreamReader sr;
        try
        {
            sr = new StreamReader(@"Veri.txt");

        }
        catch (Exception e)
        {
            Console.WriteLine("Dosya okuma hatası!");
            Console.WriteLine("Exception: " + e.Message);
            return;
        }


        otomobil otomobil = new otomobil ();
        minibus minibus = new minibus();
        otobus otobus = new otobus();
        odemeKabul odeme = new odemeKabul();

        List<otomobil> otomobils = new List<otomobil>();
        List <minibus> minibuss = new List<minibus>();
        List <otobus> otobuss = new List<otobus>();

        Random rand = new Random ();
        String time; 

        bool showcase = false, restart = true;
        string tur = "", name = "";
        float fee = 0.0f;
        int HGSnumber;
        char evethayir;
        while (!showcase)
        {
            time = DateTime.Now.ToString("HH:mm");

            sr.DiscardBufferedData();
            sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            read = sr.ReadLine();
            
            
            try
            {
                Console.Write("Okunan HGS numarasını giriniz: ");
                HGSnumber = int.Parse(Console.ReadLine());
            }catch(Exception e)
            {
                Console.WriteLine("Yanlış HGS türü!");
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("-----------------------------------------------------------------------");
                continue;
            }
            
            
            while (read != null)
            {
                read = sr.ReadLine();
                string[] strArray = read.Split("|"); // HGS Numarası | Ad Soyad | Araç Türü | Bakiye
                if (int.Parse(strArray[0]) == HGSnumber)
                {
                    name = strArray[1];
                    tur = strArray[2];
                    fee = odeme.Gise(tur, strArray[3]);
                    tur.ToLower();
                    break;
                }
                if(sr.ReadLine == null)
                {
                    sr.DiscardBufferedData();
                    Console.WriteLine("Girilen türde HGS numarası sistemde bulunamadı! Lütfen tekrar deneyin.");
                    Console.Write("Okunan HGS numarasını giriniz: ");
                    HGSnumber = int.Parse(Console.ReadLine());
                    read = sr.ReadLine();  
                }
            }


            if (tur.Equals("otomobil"))
            {
                otomobils.Add(otomobil.Assign(HGSnumber, name, fee));
            }
            else if (tur.Equals("otobüs"))
            {
                otobuss.Add(otobus.Assign(HGSnumber, name, fee));
            }
            else
            {
                minibuss.Add(minibus.Assign(HGSnumber, name, fee));
            }

            if (time.Equals("00:00"))
                break;

            while (true)
            {
                Console.Write("Günü Bitirmek ister misiniz? E/H : ");
                try
                {
                    evethayir = char.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine("Boş veri girildi! Lütfen \'e\' veya \'h\' yazdığınızdan emin olun!");
                    Console.WriteLine("Exception: " + e.Message);
                    continue;
                }
                
                if (evethayir.Equals('e') || evethayir.Equals('h'))
                {
                    if (evethayir.Equals('e'))
                        showcase = true;
                    break;
                }
                    
                Console.WriteLine("Yanlış girdiniz. Lütfen \'e\' veya \'h\' yazınız");
            }

            
            Console.WriteLine("-----------------------------------------------------------------------");
            
        }
        Console.WriteLine("Boğaz köprüsünden geçen araçlar: ");
        foreach (var oto in otobuss)
            otobus.Print(oto);
        foreach (var mini in minibuss)
            minibus.Print(mini);
        foreach (var bil in otomobils)
            otomobil.Print(bil);

        while (true)
        {
            Console.Write("Bugünün kazancını öğrenmek ister misiniz? E/H : ");
            try
            {
                evethayir = char.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Boş veri girildi! Lütfen \'e\' veya \'h\' yazdığınızdan emin olun!");
                Console.WriteLine("Exception: " + e.Message);
                continue;
            }
            if (evethayir.Equals('e') || evethayir.Equals('h'))
            {
                if (evethayir.Equals('e'))
                {
                    totalBakiye total = new totalBakiye();
                    total.printBakiye();
                }
                    
                break;
            }

            Console.WriteLine("Yanlış girdiniz. Lütfen \'e\' veya \'h\' yazınız");
        }

        sr.Close();
    }
}

class otomobil
{
    int HGS;
    string sahip;
    float bakiye;
    DateTime gecisZamani;
    public otomobil Assign(int hgs, string nameSname, float fee)
    {
        otomobil car = new otomobil();
        car.HGS = hgs;
        car.sahip = nameSname;
        car.bakiye = fee;
        car.gecisZamani = DateTime.Now;
        return car;
    }
    public void Print(otomobil car)
    {
        Console.WriteLine("OTOMOBİL");
        Console.WriteLine("Araç sahibinin adı: " + car.sahip);
        Console.WriteLine("HGS numarası: " + car.HGS);
        Console.WriteLine("Kalan bakiye: " + car.bakiye);
        Console.WriteLine("Geçiş zamanı: " + car.gecisZamani);
        Console.WriteLine("-----------------------------------------------------------------------");

    }
}

class minibus
{
    int HGS;
    string sahip;
    float bakiye;
    DateTime gecisZamani;
    public minibus Assign(int hgs, string nameSname, float fee)
    {
        minibus car = new minibus();
        car.HGS = hgs;
        car.sahip = nameSname;
        car.bakiye = fee;
        car.gecisZamani = DateTime.Now;
        return car;
    }
    public void Print(minibus car)
    {
        Console.WriteLine("MİNİBÜS");
        Console.WriteLine("Araç sahibinin adı: " + car.sahip);
        Console.WriteLine("HGS numarası: " + car.HGS);
        Console.WriteLine("Kalan bakiye: " + car.bakiye);
        Console.WriteLine("Geçiş zamanı: " + car.gecisZamani);
        Console.WriteLine("-----------------------------------------------------------------------");

    }
}

class otobus
{
    int HGS;
    string sahip;
    float bakiye;
    DateTime gecisZamani;
    public otobus Assign(int hgs, string nameSname, float fee)
    {
        otobus car = new otobus();
        car.HGS = hgs;
        car.sahip = nameSname;
        car.bakiye = fee;
        car.gecisZamani = DateTime.Now;
        return car;
    }

    public void Print(otobus car)
    {
        Console.WriteLine("OTOBÜS");
        Console.WriteLine("Araç sahibinin adı: " + car.sahip);
        Console.WriteLine("HGS numarası: " + car.HGS);
        Console.WriteLine("Kalan bakiye: " + car.bakiye);
        Console.WriteLine("Geçiş zamanı: " + car.gecisZamani);
        Console.WriteLine("-----------------------------------------------------------------------");

    }
}

class odemeKabul
{
    float ucret;
    public float Gise(string arac, string aracBakiye)
    {
        totalBakiye total = new totalBakiye();
        float bakiye = float.Parse(aracBakiye);
        if (arac.Equals("otomobil"))
        {
            ucret = bakiye - 32;
            total.addBakiye(32);
        }
        else if (arac.Equals("otobüs"))
        {
            ucret = bakiye - 60;
            total.addBakiye(60);
        }
        else
        {
            ucret = bakiye - 40;
            total.addBakiye(40);
        }
        if(ucret < 0)
        {
            return -1;
        }
        return ucret;
    }
}

class totalBakiye
{
    static int total = 0;
    public void addBakiye(int ucret)
    {
        total += ucret;      
    }
    public void printBakiye()
    {
        Console.WriteLine("Bugünün kazancı: " + total);    
    }
}

